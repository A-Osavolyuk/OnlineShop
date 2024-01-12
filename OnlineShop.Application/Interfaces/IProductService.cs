using LanguageExt.Common;
using OnlineShop.Domain.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Interfaces
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
