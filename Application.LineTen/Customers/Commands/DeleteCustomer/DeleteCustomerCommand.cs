using MediatR;

namespace Application.LineTen.Customers.Commands.DeleteCustomer
{
    public sealed record DeleteCustomerCommand(Guid ID) : IRequest;
}
