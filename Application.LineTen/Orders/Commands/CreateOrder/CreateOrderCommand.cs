using Application.LineTen.Orders.DTOs;
using Domain.LineTen.Customers;
using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommand : IRequest<OrderDTO>
    {
        public Guid CustomerID { get; set; }
        public Guid ProductID { get; set; }
    }
}
