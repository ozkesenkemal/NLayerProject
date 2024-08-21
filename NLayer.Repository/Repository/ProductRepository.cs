using Microsoft.EntityFrameworkCore;
using NLayer.Data.Entity;
using NLayer.Data.Repository;

namespace NLayer.Repository.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            // Eager Loading
            return await _appDbContext.Product.Include(x => x.Category).ToListAsync();
        }
    }
}
