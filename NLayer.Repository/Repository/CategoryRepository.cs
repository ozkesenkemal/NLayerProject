using Microsoft.EntityFrameworkCore;
using NLayer.Data.Entity;
using NLayer.Data.Repository;

namespace NLayer.Repository.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProductAsync(int categoryId)
        {
            return await _appDbContext.Category
                .Include(x => x.Products)
                .Where(x => x.Id == categoryId)
                .FirstOrDefaultAsync();
        }
    }
}
