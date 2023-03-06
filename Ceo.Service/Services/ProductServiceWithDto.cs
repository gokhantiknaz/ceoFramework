using Ceo.Core.Dtos;
using Ceo.Core.Models;
using Ceo.Core.Repositories;
using Ceo.Core.Services;
using Ceo.Core.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Ceo.Service.Services
{

    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDto>, IProductServicesWithDto
    {
        private readonly IProductRepository _productRepository;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;
        public ProductServiceWithDto(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository,unitOfWork, mapper)
        {
            _productRepository = productRepository;
           // _unitOfWork = unitOfWork;
            //_mapper = mapper;
        }



        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto dto)
        {
            var newEntity = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<ProductDto>(newEntity);
            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<IEnumerable<ProductDto>>> AddRangeAsync(IEnumerable<ProductCreateDto> dto)
        {
            var newEntities = _mapper.Map<IEnumerable<Product>>(dto);
            await _productRepository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();
            var newDtos = _mapper.Map<IEnumerable<ProductDto>>(newEntities);
            return CustomResponseDto<IEnumerable<ProductDto>>.Success(StatusCodes.Status200OK, newDtos);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductsWithCategoryAsync();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
