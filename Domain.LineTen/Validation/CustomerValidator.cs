using FluentValidation;

namespace Domain.LineTen.Validation
{
    public class CustomerValidator : AbstractValidator<Entities.Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.FirstName).NotEmpty();
            RuleFor(customer => customer.FirstName).MaximumLength(50);

            RuleFor(customer => customer.LastName).NotEmpty();
            RuleFor(customer => customer.LastName).MaximumLength(50);

            RuleFor(customer => customer.Phone).NotEmpty();
            RuleFor(customer => customer.Phone).MaximumLength(20);

            RuleFor(customer => customer.Email).NotEmpty();
            RuleFor(customer => customer.Email).MaximumLength(100);
            RuleFor(customer => customer.Email).EmailAddress();
        }
    }
}
