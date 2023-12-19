using Application.LineTen.Orders.DTOs;
using MediatR;

namespace Application.LineTen.Orders.Queries.GetOrderByID
{
    public sealed class GetOrderByIDQuery : IRequest<OrderDTO>
    {
        public Guid ID { get; set; }
    }
}
