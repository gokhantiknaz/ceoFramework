using AltYapi.Core.Dtos;
using AltYapi.Core.Models;
using AltYapi.Core.Repositories;
using AltYapi.Core.Services;
using AltYapi.Core.UnitOfWorks;
using AltYapi.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.Service.Services
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
