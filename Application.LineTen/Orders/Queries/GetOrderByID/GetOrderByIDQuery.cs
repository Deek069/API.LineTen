using Application.LineTen.Orders.DTOs;
using MediatR;

namespace Application.LineTen.Orders.Queries.GetOrderByID
{
    public sealed record GetOrderByIDQuery(Guid ID) : IRequest<OrderDTO>;
}
