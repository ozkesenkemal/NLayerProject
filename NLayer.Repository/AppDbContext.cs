using Microsoft.EntityFrameworkCore;
using NLayer.Data.Entity;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductFeature> ProductFeature { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Apply all configurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //Seed data from model creating
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                Id = 1,
                Color = "Red",
                Heigth = 100,
                Weigth = 200,
                ProductId = 1
            },
            new ProductFeature()
            {
                Id = 2,
                Color = "Blue",
                Heigth = 100,
                Weigth = 200,
                ProductId = 2
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
