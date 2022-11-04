using AltYapi.Core;
using AltYapi.Core.Models;
using AltYapi.Repository.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            //Seeds de ProductFeatureSeed yapmadık buradan da yapabilirz göstermek için Migrotion esnasında veritabanına datayı direk atıyor.
            //Best practice açısından burada yazmamız gerekir örnek için yapıldı.
            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature() { Id = 1, Color = "Kırmızı", Height = 100, Width = 200, ProductId = 1 },
                new ProductFeature() { Id = 2, Color = "Mavi", Height = 100, Width = 200, ProductId = 2 } );
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //Tek tek yapmak istersek aşağıdaki gibi yaparız yukarıdaki kod Assemblydeki tüm inteface leri bulup yapar
            //modelBuilder.ApplyConfiguration (new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
