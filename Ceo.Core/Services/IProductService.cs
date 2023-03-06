using Ceo.Core.Dtos;
using Ceo.Core.Models;

namespace Ceo.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();

    }
}
