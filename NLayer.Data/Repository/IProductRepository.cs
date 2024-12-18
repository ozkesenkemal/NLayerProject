using NLayer.Data.Dto;
using NLayer.Data.Entity;

namespace NLayer.Data.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();
    }
}
