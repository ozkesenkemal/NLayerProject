using NLayer.Data.Dto;
using NLayer.Data.Entity;

namespace NLayer.Data.Service
{
    public interface ICategoryService : IService<Category>
    {
        Task<CustomResponseDto<CategoryWithProductDto>> GetSingleCategoryByIdWithProductAsync(int category);
    }
}
