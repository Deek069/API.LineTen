using Domain.LineTen.ValueObjects.Orders;

namespace Application.LineTen.Orders.Exceptions
{
    public sealed class OrderValidationException : Exception
    {
        public OrderValidationException(string message)
            : base(message)
        {
        }
    }
}
