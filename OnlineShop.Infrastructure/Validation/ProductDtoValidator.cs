using FluentValidation;
using OnlineShop.Domain.DTOs;

namespace OnlineShop.Infrastructure.Validation
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name cannot be empty")
                .MinimumLength(3).WithMessage("Product name must contain at least 3 letters")
                .MaximumLength(32).WithMessage("Product name cannot be longer than 32 letters");
            RuleFor(x => x.ProductDescription)
                .NotEmpty().WithMessage("Product description cannot be empty")
                .MinimumLength(3).WithMessage("Product description must contain at least 3 letters")
                .MaximumLength(128).WithMessage("Product name cannot be longer than 128 letters");
            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Unit cannot be empty")
                .MinimumLength(1).WithMessage("Unit must contain at least 1 letters")
                .MaximumLength(10).WithMessage("Unit cannot be longer than 10 letters");
            RuleFor(x => x.Price)
                .GreaterThan(0.01).WithMessage("Price cannot be less than 0.01 cents")
                .LessThan(10000).WithMessage("Price cannot be higher than 10000 cents");
        }
    }
}
