using Application.LineTen.Orders.DTOs;
using Domain.LineTen.Orders;
using MediatR;

namespace Application.LineTen.Orders.Queries.GetOrderSummary
{
    public sealed class GetOrderSummaryQuery : IRequest<OrderSummaryDTO>
    {
        public Guid ID { get; set; }
    }
}
