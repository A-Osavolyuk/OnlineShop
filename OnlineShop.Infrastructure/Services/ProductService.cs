using Microsoft.Extensions.Options;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Common;
using OnlineShop.Domain.DTOs;
using OnlineShop.Domain.Enuns;

#nullable disable

namespace OnlineShop.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService baseService;
        private readonly HttpData httpData;

        public ProductService(IBaseService baseService, IOptions<HttpData> options)
        {
            this.baseService = baseService;
            this.httpData = options.Value;
        }

        public async Task<ResponseDto> Create(ProductDto product)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                Method = ApiMethods.POST,
                Url = httpData.ProductApi + "api/products",
                Data = product
            });
        }

        public async Task<ResponseDto> Delete(Guid id)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                Method = ApiMethods.DELETE,
                Url = httpData.ProductApi + $"api/products/{id}"
            });
        }

        public async Task<ResponseDto> GetAll()
        {
            return await baseService.SendAsync(new RequestDto()
            {
                Method = ApiMethods.GET,
                Url = httpData.ProductApi + "api/products"
            });
        }

        public async Task<ResponseDto> GetById(Guid id)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                Method = ApiMethods.GET,
                Url = httpData.ProductApi + $"api/products/{id}"
            });
        }

        public async Task<ResponseDto> Update(ProductDto product, Guid id)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                Method = ApiMethods.PUT,
                Url = httpData.ProductApi + $"api/products/{id}",
                Data = product
            });
        }
    }
}
