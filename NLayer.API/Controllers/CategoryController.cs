using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Data.Dto;
using NLayer.Data.Service;

namespace NLayer.API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProduct(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductAsync(categoryId));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDto));
        }
    }
}
