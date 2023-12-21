using MediatR;

namespace Application.LineTen.Customers.Commands.UpdateCustomer
{
    public sealed record UpdateCustomerCommand(
        Guid ID, 
        string FirstName, 
        string LastName, 
        string Phone, 
        string Email) : IRequest;
}
