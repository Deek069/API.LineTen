using Domain.LineTen.Customers;
using MediatR;

namespace Application.LineTen.Customers.Commands.DeleteCustomer
{
    public sealed class DeleteCustomerCommand : IRequest<bool>
    {
        public CustomerID ID { get; set; }
    }
}
