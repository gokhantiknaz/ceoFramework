using AltYapi.Core.Dtos;
using AltYapi.Core.Models;

namespace AltYapi.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();

    }
}
