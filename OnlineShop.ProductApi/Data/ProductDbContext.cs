using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductApi.Data.Entities;

namespace OnlineShop.ProductApi.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<ProductEntity> Products => Set<ProductEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity() { Price = 3, Unit = "kg", ProductName = "Apple", ProductDescription = "Red Apple" },
                new ProductEntity() { Price = 2.25, Unit = "kg", ProductName = "Banana", ProductDescription = "Columbian Banana" },
                new ProductEntity() { Price = 9.10, Unit = "kg", ProductName = "Orange", ProductDescription = "Israelian Orange" },
                new ProductEntity() { Price = 50, Unit = "l", ProductName = "Wine", ProductDescription = "Red French Wine" },
                new ProductEntity() { Price = 4.50, Unit = "kg", ProductName = "Kiwi", ProductDescription = "Australian Kiwi" },
                new ProductEntity() { Price = 12.50, Unit = "kg", ProductName = "Meat", ProductDescription = "Pork Meat" }
            );
        }
    }
}
