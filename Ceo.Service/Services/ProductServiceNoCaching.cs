using Ceo.Core.Dtos;
using Ceo.Core.Models;
using Ceo.Core.Repositories;
using Ceo.Core.Services;
using Ceo.Core.UnitOfWorks;
using AutoMapper;

namespace Ceo.Service.Services
{
    public class ProductServiceNoCaching : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productService, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductsWithCategoryAsync();

            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }

     
    }
}
