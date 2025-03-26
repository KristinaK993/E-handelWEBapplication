using FluentValidation;
using E_handelWEBapplication.DTOs;

namespace E_handelWEBapplication.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(100).WithMessage("Product name can't be longer than 100 characters");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than zero");
        }
    }
}
