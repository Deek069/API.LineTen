using FluentValidation;

namespace Domain.LineTen.Validation
{
    public class ProductValidator : AbstractValidator<Entities.Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name).NotEmpty();
            RuleFor(product => product.Name).MaximumLength(50);

            RuleFor(product => product.Description).NotEmpty();

            RuleFor(product => product.SKU).NotEmpty();
            RuleFor(product => product.SKU).MaximumLength(20);
        }
    }
}
