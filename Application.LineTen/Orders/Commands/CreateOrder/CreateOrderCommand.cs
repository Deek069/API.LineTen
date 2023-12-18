using Application.LineTen.Orders.DTOs;
using Domain.LineTen.Customers;
using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommand : IRequest<OrderDTO>
    {
        public CustomerID CustomerID { get; set; }
        public ProductID ProductID { get; set; }
    }
}
