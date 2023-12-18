using Application.LineTen.Orders.DTOs;
using MediatR;

namespace Application.LineTen.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderSummaryDTO>>
    {
    }
}
