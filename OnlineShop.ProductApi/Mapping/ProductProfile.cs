using AutoMapper;
using OnlineShop.ProductApi.Data.Entities;
using OnlineShop.ProductApi.Models.DTOs;

namespace OnlineShop.ProductApi.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, ProductEntity>();
        }
    }
}
