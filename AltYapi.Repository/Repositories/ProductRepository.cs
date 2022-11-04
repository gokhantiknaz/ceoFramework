using AltYapi.Core.Models;
using AltYapi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<Product>> GetProductsWithCategoryAsync()
        {
            //Eager Loading datayı çekerken categorileri de almasını sağladık
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
