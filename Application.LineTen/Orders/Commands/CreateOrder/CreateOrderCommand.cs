using Application.LineTen.Orders.DTOs;
using Domain.LineTen.Customers;
using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Orders.Commands.CreateOrder
{
    public sealed record CreateOrderCommand(Guid CustomerID, Guid ProductID) : IRequest<OrderDTO>;
}
