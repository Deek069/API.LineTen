using Application.LineTen.Orders.DTOs;
using MediatR;

namespace Application.LineTen.Orders.Queries.GetAllOrders
{
    public record GetAllOrdersQuery() : IRequest<IEnumerable<OrderSummaryDTO>>;
}
