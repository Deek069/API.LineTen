using Domain.LineTen.ValueObjects.Orders;

namespace Application.LineTen.Orders.Exceptions
{
    public sealed class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(OrderID id)
            : base($"The order with ID {id.value} was not found.")
        {
        }
    }
}
