using Domain.LineTen.ValueObjects.Orders;

namespace Application.LineTen.Orders.Commands.UpdateOrder
{
    public sealed record UpdateOrderRequest(OrderStatus Status);
}
