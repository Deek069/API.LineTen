using Application.LineTen.Customers.DTOs;
using MediatR;

namespace Application.LineTen.Customers.Commands.CreateCustomer
{
    public sealed class CreateCustomerCommand : IRequest<CustomerDTO>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
