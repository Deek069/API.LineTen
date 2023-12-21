using Application.LineTen.Customers.DTOs;
using MediatR;

namespace Application.LineTen.Customers.Commands.CreateCustomer
{
    public sealed record CreateCustomerCommand(string FirstName, string LastName, string Phone, string Email) : IRequest<CustomerDTO>;
}
