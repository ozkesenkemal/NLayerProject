using NLayer.Data.Dto;
using NLayer.Data.Entity;

namespace NLayer.Data.Service
{
    public interface IProductServiceWithDto : IServiceWithDto<Product, ProductDto>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();
        Task<CustomResponseDto<ProductDto>> AddAsync();
        Task<CustomResponseDto<NoContentDto>> UpdateAsync();
    }
}
