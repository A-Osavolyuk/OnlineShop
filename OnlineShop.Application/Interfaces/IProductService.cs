using OnlineShop.Domain.DTOs;

namespace OnlineShop.Application.Interfaces
{
    public interface IProductService
    {
        public Task<ResponseDto> GetAll();
        public Task<ResponseDto> GetById(Guid id);
        public Task<ResponseDto> Create(ProductDto product);
        public Task<ResponseDto> Update(ProductDto product, Guid id);
        public Task<ResponseDto> Delete(Guid id);
    }
}
