using FluentValidation;
using OnlineShop.ProductApi.Models.DTOs;

namespace OnlineShop.ProductApi.Validation
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotNull().NotEmpty().NotEqual("string").MinimumLength(3).MaximumLength(32);
            RuleFor(x => x.ProductDescription).NotNull().NotEmpty().NotEqual("string").MinimumLength(3).MaximumLength(128);
            RuleFor(x => x.Unit).NotNull().NotEmpty().NotEqual("string").MaximumLength(1).MaximumLength(10);
            RuleFor(x => x.Price).GreaterThan(0.01).LessThan(10000);
        }
    }
}
