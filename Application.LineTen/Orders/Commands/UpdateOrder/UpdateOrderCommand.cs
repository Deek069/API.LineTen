using Domain.LineTen.Orders;
using MediatR;

namespace Application.LineTen.Orders.Commands.UpdateOrder
{
    public sealed record UpdateOrderCommand(Guid ID, OrderStatus Status) : IRequest;
}
