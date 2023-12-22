using FluentValidation;

namespace Domain.LineTen.Validation
{
    public class OrderValidator : AbstractValidator<Entities.Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.CustomerID).NotEmpty();
            RuleFor(order => order.ProductID).NotEmpty();
            RuleFor(order => order.Status).NotEmpty();
        }
    }
}
