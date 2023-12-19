using Domain.LineTen.Customers;
using MediatR;

namespace Application.LineTen.Customers.Commands.UpdateCustomer
{
    public sealed class UpdateCustomerCommand : IRequest<bool>
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
