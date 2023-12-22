using MediatR;

namespace Application.LineTen.Orders.Commands.DeleteOrder
{
    public sealed record DeleteOrderCommand(Guid ID) : IRequest;
}
