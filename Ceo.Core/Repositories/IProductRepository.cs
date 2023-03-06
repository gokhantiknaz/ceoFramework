using AltYapi.Core.Models;

namespace AltYapi.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategoryAsync();
    }
}
