using Microsoft.AspNetCore.Mvc;
using NLayer.Data.Dto;
using NLayer.Data.Entity;
using NLayer.Data.Service;

namespace NLayer.API.Controllers
{
    public class CategoryWithDtoController : BaseController
    {
        private readonly IServiceWithDto<Category, CategoryDto> _categoryWithServiceDto;

        public CategoryWithDtoController(IServiceWithDto<Category, CategoryDto> categoryWithServiceDto)
        {
            _categoryWithServiceDto = categoryWithServiceDto;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _categoryWithServiceDto.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            return CreateActionResult(await _categoryWithServiceDto.AddAsync(categoryDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            return CreateActionResult(await _categoryWithServiceDto.UpdateAsync(categoryDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _categoryWithServiceDto.DeleteAsync(id));
        }
    }
}
