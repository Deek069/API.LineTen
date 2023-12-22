using Application.LineTen.Orders.DTOs;
using MediatR;

namespace Application.LineTen.Orders.Commands.CreateOrder
{
    public sealed record CreateOrderCommand(Guid CustomerID, Guid ProductID) : IRequest<OrderDTO>;
}
