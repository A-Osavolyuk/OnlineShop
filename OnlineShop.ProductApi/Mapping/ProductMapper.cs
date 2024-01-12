using OnlineShop.ProductApi.Data.Entities;
using OnlineShop.ProductApi.Models.DTOs;
using Riok.Mapperly.Abstractions;

namespace OnlineShop.ProductApi.Mapping
{
    [Mapper]
    public partial class ProductMapper
    {
        public partial ProductDto FromProductEntityToProductDto(ProductEntity productDto);
        public partial ProductEntity FromProductDtoToProductEntity(ProductDto productDto);
    }
}
