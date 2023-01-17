using AltYapi.Core.Dtos;
using AltYapi.Core.Models;

namespace AltYapi.Core.Services
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
