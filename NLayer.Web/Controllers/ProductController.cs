using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.API.Filter;
using NLayer.Data.Dto;
using NLayer.Data.Entity;

namespace NLayer.Web.Controllers
{
    public class ProductController : Controller
    {
        /*
         private readonly IProductService _productService;
         private readonly ICategoryService _categoryService;
         private readonly IMapper _mapper;

         public ProductController(ProductApiService productService, CategoryApiService categoryService, IMapper mapper)
         {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
         }
         */
        private readonly ProductApiService _productService;
        private readonly CategoryApiService _categoryService;

        public ProductController(ProductApiService productService, CategoryApiService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        /*
         public async IActionResult Index()
         {
            return View(await _productService.GetProductsWithCategoryWeb());
         }
         */

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetProductsWithCategoryAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.SaveAsync(productDto);
                //await _productService.AddAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>(categories.ToList());
            ViewBag.Categories = new SelectList(categories, "Id", "Name");//new SelectList(categoriesDto, "Id", "Name");

            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var categories = await _categoryService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>(categories.ToList());
            ViewBag.Categories = new SelectList(categories, "Id", product.CategoryId);//new SelectList(categoriesDto, "Id", product.CategoryId);
            return View(product);//(_mapper.Map<ProductDto>(product));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(productDto); //(_mapper.Map<Product>(productDto)); 
                return RedirectToAction(nameof(Index));
            } 
            var categories = await _categoryService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>(categories.ToList());
            ViewBag.Categories = new SelectList(categories, "Id", "Name", productDto.CategoryId);//new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var product = await _productService.GetByIdAsync(id);
            await _productService.DeleteAsync(id);//(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
