using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AltYapi.Repository.AutoHistory
{
    public static class ModelBuilderExtensions
    {
        private const int DefaultChangedMaxLength = 2048;

        //
        // Summary:
        //     Enables the automatic recording change history.
        //
        // Parameters:
        //   modelBuilder:
        //     The Microsoft.EntityFrameworkCore.ModelBuilder to enable auto history feature.
        //
        //   changedMaxLength:
        //     The maximum length of the 'Changed' column. null will use default setting 2048.
        //
        //   JsonSerializerOptions:
        //     The json setting for the 'Changed' column
        //
        // Returns:
        //     The Microsoft.EntityFrameworkCore.ModelBuilder had enabled auto history feature.
        public static ModelBuilder EnableAutoHistory(this ModelBuilder modelBuilder, int? changedMaxLength = null, JsonSerializerOptions JsonSerializerOptions = null)
        {
            return modelBuilder.EnableAutoHistory<AutoHistory>(delegate (AutoHistoryOptions o)
            {
                o.ChangedMaxLength = changedMaxLength;
                o.LimitChangedLength = false;
                o.JsonSerializerOptions = JsonSerializerOptions;
            });
        }

        public static ModelBuilder EnableAutoHistory<TAutoHistory>(this ModelBuilder modelBuilder, Action<AutoHistoryOptions> configure) where TAutoHistory : AutoHistory
        {
            AutoHistoryOptions options = AutoHistoryOptions.Instance;
            configure?.Invoke(options);
            modelBuilder.Entity(delegate (EntityTypeBuilder<TAutoHistory> b)
            {
                b.Property((TAutoHistory c) => c.RowId).IsRequired().HasMaxLength(options.RowIdMaxLength);
                b.Property((TAutoHistory c) => c.TableName).IsRequired().HasMaxLength(options.TableMaxLength);
                if (options.LimitChangedLength)
                {
                    int num = options.ChangedMaxLength ?? 2048;
                    if (num <= 0)
                    {
                        num = 2048;
                    }

                    b.Property((TAutoHistory c) => c.Changed).HasMaxLength(num);
                }
            });
            return modelBuilder;
        }
    }
}
