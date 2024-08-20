using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Data.Entity;

namespace NLayer.Repository.Seed
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product() { Id = 1, CategoryId = 1, Name = "Kalem 1", Price = 100, Stock = 50, InsertDate = DateTime.UtcNow },
                new Product() { Id = 2, CategoryId = 2, Name = "Kitap 1", Price = 200, Stock = 150, InsertDate = DateTime.UtcNow },
                new Product() { Id = 3, CategoryId = 2, Name = "Defter 1", Price = 300, Stock = 250, InsertDate = DateTime.UtcNow });
        }
    }
}
