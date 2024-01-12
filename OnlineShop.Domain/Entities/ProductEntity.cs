using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.Entities
{
    public class ProductEntity
    {
        public Guid ProductId { get; set; } = Guid.NewGuid();
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string ProductDescription { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        [Required]
        public string Unit { get; set; } = string.Empty;
    }
}
