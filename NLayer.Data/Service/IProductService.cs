using NLayer.Data.Dto;
using NLayer.Data.Entity;

namespace NLayer.Data.Service
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<ProductWithCategoryDto>> GetProductsWithCategory();
    }
}
