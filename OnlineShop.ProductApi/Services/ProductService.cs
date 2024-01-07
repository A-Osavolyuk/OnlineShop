using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductApi.Data;
using OnlineShop.ProductApi.Data.Entities;
using OnlineShop.ProductApi.Models.DTOs;
using OnlineShop.ProductApi.Services.Interfaces;

namespace OnlineShop.ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext dbContext;

        public ProductService(
            ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Result<ProductEntity>> Create(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<ProductEntity>>> GetAll()
        {
            var departments = await dbContext.Products.ToListAsync();
            return new Result<IEnumerable<ProductEntity>>(departments);
        }

        public Task<Result<ProductEntity>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ProductEntity>> Update(ProductDto product, int id)
        {
            throw new NotImplementedException();
        }
    }
}
