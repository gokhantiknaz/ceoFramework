using AltYapi.Core.Dtos;
using AltYapi.Core.Models;
using AltYapi.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AltYapi.API.Controllers
{
    public class ProductsWithDtoController : CustomBaseController
    {
        private readonly IProductServicesWithDto _productServiceWithDto;
        private readonly ILogger<ProductsWithDtoController> _logger;

        public ProductsWithDtoController(IProductServicesWithDto productServiceWithDto, ILogger<ProductsWithDtoController> logger)
        {
            _productServiceWithDto = productServiceWithDto;
            _logger = logger;
        }

        //GET /api/products/GetProductsWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
          
            return CreateActionResult(await _productServiceWithDto.GetProductsWithCategory());

        }

        //Generic olduğu için mapping i burda yazıyoruz.
        //GET /api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _productServiceWithDto.GetAllAsync());
        }

        //GET /api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _productServiceWithDto.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto productDto)
        {
            return CreateActionResult(await _productServiceWithDto.AddAsync(productDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            return CreateActionResult(await _productServiceWithDto.UpdateAsync(productDto));
        }

        //DELETE /api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {

            return CreateActionResult(await _productServiceWithDto.RemoveAsync(id));

        }


        [HttpPost("SaveAll")]
        public async Task<IActionResult> SaveAll(List<ProductCreateDto> productsDto)
        {
            return CreateActionResult(await _productServiceWithDto.AddRangeAsync(productsDto));
        }

        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> RemoveAll(List<int> ids)
        {
            return CreateActionResult(await _productServiceWithDto.RemoveRangeAsync(ids));
        }

        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _productServiceWithDto.AnyAsync(x=>x.Id==id));
        }
    }
}
