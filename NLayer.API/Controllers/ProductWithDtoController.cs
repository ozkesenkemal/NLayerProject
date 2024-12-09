using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filter;
using NLayer.Data.Dto;
using NLayer.Data.Entity;
using NLayer.Data.Service;

namespace NLayer.API.Controllers
{
    public class ProductWithDtoController : BaseController
    {
        private readonly IProductServiceWithDto _productWithServiceDto;

        public ProductWithDtoController(IProductServiceWithDto productWithServiceDto)
        {
            _productWithServiceDto = productWithServiceDto;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _productWithServiceDto.GetProductsWithCategory());
        }

        [HttpGet()]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _productWithServiceDto.GetAllAsync());
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById()
        {
            return CreateActionResult(await _productWithServiceDto.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            return CreateActionResult(await _productWithServiceDto.AddAsync(productDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            return CreateActionResult(await _productWithServiceDto.UpdateAsync(productDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _productWithServiceDto.DeleteAsync(id));
        }

        [HttpPost("SaveAll")]
        public async Task<IActionResult> Save(List<ProductDto> productDtoList)
        {
            return CreateActionResult(await _productWithServiceDto.AddRangeAsync(productDtoList));
        }

        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll(List<int> idList)
        {
            return CreateActionResult(await _productWithServiceDto.DeleteRangeAsync(idList));
        }

        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _productWithServiceDto.AnyAsync(x => x.Id == id));
        }
    }
}
