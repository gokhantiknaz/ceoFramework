using Ceo.Core;
using Ceo.Core.Models;
using Ceo.Repository.AutoHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using System.Xml;

namespace Ceo.Repository
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<Person> People { get; set; }


        public override int SaveChanges()
        {
            UpdateChangeTracker();

            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            UpdateChangeTracker();

            this.EnsureAutoHistory();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void UpdateChangeTracker()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
                                entityReference.CreatedDate = DateTime.Now;
                                //Log işlemleri için OriginalValue Setlendi
                                Entry(entityReference).Property(x => x.CreatedDate).OriginalValue = entityReference.CreatedDate;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                entityReference.UpdatedDate = DateTime.Now;
                                //Log işlemleri için OriginalValue Setlendi
                                Entry(entityReference).Property(x => x.UpdatedDate).OriginalValue = entityReference.UpdatedDate;
                                break;
                            }
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Deleted:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seeds de ProductFeatureSeed yapmadık buradan da yapabilirz göstermek için Migrotion esnasında veritabanına datayı direk atıyor.
            //Best practice açısından burada yazmamız gerekir örnek için yapıldı.
            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature() { Id = 1, Color = "Kırmızı", Height = 100, Width = 200, ProductId = 1 },
                new ProductFeature() { Id = 2, Color = "Mavi", Height = 100, Width = 200, ProductId = 2 });
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //Tek tek yapmak istersek aşağıdaki gibi yaparız yukarıdaki kod Assemblydeki tüm inteface leri bulup yapar
            //modelBuilder.ApplyConfiguration (new ProductConfiguration());
            //base.OnModelCreating(modelBuilder.EnableAutoHistory(null));
            base.OnModelCreating(modelBuilder);

        }

    }
}
