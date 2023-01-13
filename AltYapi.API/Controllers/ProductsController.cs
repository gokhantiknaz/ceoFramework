
using AltYapi.API.Filters;
using AltYapi.Core.Dtos;
using AltYapi.Core.Models;
using AltYapi.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AltYapi.API.Controllers
{
    [ServiceFilter(typeof(NotFoundFilter<Product>))]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }



        //GET /api/products/GetProductsWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _productService.GetProductsWithCategory());

        }


        //Generic olduğu için mapping i burda yazıyoruz.
        //GET /api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productService.GetAllAsync();

            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            // return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }


        //GET /api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));

        }


        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            product = await _productService.AddAsync(product);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));

        }



        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productService.UpdateAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRange(IEnumerable<ProductUpdateDto> productDtos)
        {
            var products = _mapper.Map<IEnumerable<Product>>(productDtos);
            await _productService.UpdateRangeAsync(products);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpPatch]
        public async Task<IActionResult> Update(int id, JsonPatchDocument product)
        {
            var productReplace = await _productService.GetByIdAsync(id);
            if (productReplace != null)
            {
                product.ApplyTo(productReplace);
            }
            await _productService.UpdateAsync(productReplace);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        //DELETE /api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var products = await _productService.GetByIdAsync(id);

            //İlerde göreceğiz sileriz
            if (products == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Bu id ye sahip ürün bulunamadı!"));
            }
            await _productService.RemoveAsync(products);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }



    }
}
