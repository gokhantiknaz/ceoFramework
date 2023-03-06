using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Text.Json;

namespace Ceo.Repository.AutoHistory
{
    public static class DbContextExtensions2
    {
       

        //
        // Summary:
        //     Ensures the automatic history.
        //
        // Parameters:
        //   context:
        //     The context.
        public static void EnsureAutoHistory(this DbContext context)
        {
            context.EnsureAutoHistory(() => new ChangeLog());
        }

        public static void EnsureAutoHistory<TAutoHistory>(this DbContext context, Func<TAutoHistory> createHistoryFactory) where TAutoHistory : ChangeLog
        {
            EntityEntry[] array = (from e in context.ChangeTracker.Entries()
                                   where e.State == EntityState.Modified || e.State == EntityState.Deleted
                                   select e).ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                TAutoHistory val = array[i].ChangeLog(createHistoryFactory);
                if (val != null)
                {
                    ElasticSearch.CheckExistsAndInsert(val);
                   // context.Add(val);
                }
            }
        }

        internal static TAutoHistory ChangeLog<TAutoHistory>(this EntityEntry entry, Func<TAutoHistory> createHistoryFactory) where TAutoHistory : ChangeLog
        {
            if (IsEntityExcluded(entry))
            {
                return null;
            }

            IEnumerable<PropertyEntry> propertiesWithoutExcluded = GetPropertiesWithoutExcluded(entry);
            if (!propertiesWithoutExcluded.Any((PropertyEntry p) => p.IsModified) && entry.State != EntityState.Deleted)
            {
                return null;
            }

            TAutoHistory val = createHistoryFactory();
            val.EntityName = entry.Metadata.GetTableName();
            val.DateChanged= DateTime.Now;
            switch (entry.State)
            {
                case EntityState.Added:
                    WriteHistoryAddedState(val, propertiesWithoutExcluded);
                    break;
                case EntityState.Modified:
                    WriteHistoryModifiedState(val, entry, propertiesWithoutExcluded);
                    break;
                case EntityState.Deleted:
                    WriteHistoryDeletedState(val, entry, propertiesWithoutExcluded);
                    break;
                default:
                    throw new NotSupportedException("ChangeLog only support Deleted and Modified entity.");
            }

            return val;
        }

        private static bool IsEntityExcluded(EntityEntry entry)
        {
            return entry.Metadata.ClrType.GetCustomAttributes(typeof(ExcludeFromHistoryAttribute), inherit: true).Any();
        }

        private static IEnumerable<PropertyEntry> GetPropertiesWithoutExcluded(EntityEntry entry)
        {
            IEnumerable<string> excludedProperties = from p in entry.Metadata.ClrType.GetProperties()
                                                     where p.GetCustomAttributes(typeof(ExcludeFromHistoryAttribute), inherit: true).Count() > 0
                                                     select p.Name;
            return entry.Properties.Where((PropertyEntry f) => !excludedProperties.Contains(f.Metadata.Name));
        }

        //
        // Summary:
        //     Ensures the history for added entries
        //
        // Parameters:
        //   context:
        //
        //   addedEntries:
        public static void EnsureAddedHistory(this DbContext context, EntityEntry[] addedEntries)
        {
            context.EnsureAddedHistory(() => new ChangeLog(), addedEntries);
        }

        public static void EnsureAddedHistory<TAutoHistory>(this DbContext context, Func<TAutoHistory> createHistoryFactory, EntityEntry[] addedEntries) where TAutoHistory : ChangeLog
        {
            for (int i = 0; i < addedEntries.Length; i++)
            {
                TAutoHistory val = addedEntries[i].ChangeLog(createHistoryFactory);
                if (val != null)
                {
                    context.Add(val);
                }
            }
        }

        internal static TAutoHistory AddedHistory<TAutoHistory>(this EntityEntry entry, Func<TAutoHistory> createHistoryFactory) where TAutoHistory : ChangeLog
        {
            if (IsEntityExcluded(entry))
            {
                return null;
            }

            IEnumerable<PropertyEntry> propertiesWithoutExcluded = GetPropertiesWithoutExcluded(entry);
            dynamic val = new ExpandoObject();
            foreach (PropertyEntry item in propertiesWithoutExcluded)
            {
                ((IDictionary<string, object>)val)[item.Metadata.Name] = ((item.OriginalValue != null) ? item.OriginalValue : null);
            }

            TAutoHistory val2 = createHistoryFactory();
            val2.EntityName = entry.Metadata.GetTableName();
            val2.PrimeryKey = entry.PrimaryKey();
            val2.DateChanged=DateTime.Now;
            val2.State = EntityState.Added;
            val2.Changed = JsonSerializer.Serialize(val, AutoHistoryOptions.Instance.JsonSerializerOptions);
            return val2;
        }

        private static string PrimaryKey(this EntityEntry entry)
        {
            IKey key = entry.Metadata.FindPrimaryKey();
            List<object> list = new List<object>();
            foreach (IProperty property in key!.Properties)
            {
                object currentValue = entry.Property(property.Name).CurrentValue;
                if (currentValue != null)
                {
                    list.Add(currentValue);
                }
            }

            return string.Join(",", list);
        }

        private static void WriteHistoryAddedState(ChangeLog history, IEnumerable<PropertyEntry> properties)
        {
            dynamic val = new ExpandoObject();
            foreach (PropertyEntry property in properties)
            {
                if (!property.Metadata.IsKey() && !property.Metadata.IsForeignKey())
                {
                    ((IDictionary<string, object>)val)[property.Metadata.Name] = property.CurrentValue;
                }
            }

            history.PrimeryKey = "0";
            history.State = EntityState.Added;
            history.Changed = JsonSerializer.Serialize(val);
        }

        private static void WriteHistoryModifiedState(ChangeLog history, EntityEntry entry, IEnumerable<PropertyEntry> properties)
        {
            dynamic val = new ExpandoObject();
            dynamic val2 = new ExpandoObject();
            dynamic val3 = new ExpandoObject();
            PropertyValues propertyValues = null;
            foreach (PropertyEntry property in properties)
            {
                if (!property.IsModified)
                {
                    continue;
                }

                if (property.OriginalValue != null)
                {
                    if (!property.OriginalValue!.Equals(property.CurrentValue))
                    {
                        ((IDictionary<string, object>)val2)[property.Metadata.Name] = property.OriginalValue;
                    }
                    else
                    {
                        if (propertyValues == null)
                        {
                            propertyValues = entry.GetDatabaseValues();
                        }

                        object value = propertyValues.GetValue<object>(property.Metadata.Name);
                        ((IDictionary<string, object>)val2)[property.Metadata.Name] = value;
                    }
                }
                else
                {
                    ((IDictionary<string, object>)val2)[property.Metadata.Name] = null;
                }

                ((IDictionary<string, object>)val3)[property.Metadata.Name] = property.CurrentValue;
            }

            ((IDictionary<string, object>)val)["before"] = (object)val2;
            ((IDictionary<string, object>)val)["after"] = (object)val3;
            history.PrimeryKey = entry.PrimaryKey();
            history.State = EntityState.Modified;
            history.Changed = JsonSerializer.Serialize(val, AutoHistoryOptions.Instance.JsonSerializerOptions);
        }

        private static void WriteHistoryDeletedState(ChangeLog history, EntityEntry entry, IEnumerable<PropertyEntry> properties)
        {
            dynamic val = new ExpandoObject();
            foreach (PropertyEntry property in properties)
            {
                ((IDictionary<string, object>)val)[property.Metadata.Name] = property.OriginalValue;
            }

            history.PrimeryKey = entry.PrimaryKey();
            history.State = EntityState.Deleted;
            history.Changed = JsonSerializer.Serialize(val, AutoHistoryOptions.Instance.JsonSerializerOptions);
        }
    }
}
