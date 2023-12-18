using Application.LineTen.Customers.DTOs;
using Domain.LineTen.Customers;
using MediatR;

namespace Application.LineTen.Customers.Queries.GetCustomerByID
{
    public sealed class GetCustomerByIDQuery : IRequest<CustomerDTO>
    {
        public CustomerID ID { get; set; }
    }
}
