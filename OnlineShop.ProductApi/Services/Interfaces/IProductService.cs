using LanguageExt.Common;
using OnlineShop.ProductApi.Data.Entities;
using OnlineShop.ProductApi.Models.DTOs;

namespace OnlineShop.ProductApi.Services.Interfaces
{
    public interface IProductService
    {
        public Task<Result<IEnumerable<ProductEntity>>> GetAll();
        public Task<Result<ProductEntity>> GetById(Guid id);
        public Task<Result<ProductEntity>> Create(ProductDto product);
        public Task<Result<ProductEntity>> Update(ProductDto product, Guid id);
        public Task<Result<bool>> Delete(Guid id);
    }
}
