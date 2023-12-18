using Application.LineTen.Orders.DTOs;
using Domain.LineTen.Orders;
using MediatR;

namespace Application.LineTen.Orders.Queries.GetOrderByID
{
    public sealed class GetOrderByIDQuery : IRequest<OrderDTO>
    {
        public OrderID OrderID { get; set; }
    }
}
