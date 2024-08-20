using NLayer.Data.Entity;

namespace NLayer.Data.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetSingleCategoryByIdWithProductAsync(int category);
    }
}
