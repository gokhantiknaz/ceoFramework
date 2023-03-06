using Ceo.Core.Dtos;
using Ceo.Core.Models;

namespace Ceo.Core.Services
{
    public interface IProductServicesWithDto:IServiceWithDto<Product, ProductDto>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();

        //Overload ediyoruz
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto);
        //Overload ediyoruz
        Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto dto);

        Task<CustomResponseDto<IEnumerable<ProductDto>>> AddRangeAsync(IEnumerable< ProductCreateDto> dto);

    }
}
