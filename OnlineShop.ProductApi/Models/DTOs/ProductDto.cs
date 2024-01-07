using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ProductApi.Models.DTOs
{
    public class ProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
