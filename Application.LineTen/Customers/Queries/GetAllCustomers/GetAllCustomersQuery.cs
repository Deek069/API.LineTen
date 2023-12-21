using Application.LineTen.Customers.DTOs;
using MediatR;

namespace Application.LineTen.Customers.Queries.GetAllCustomers
{
    public record GetAllCustomersQuery() : IRequest<IEnumerable<CustomerDTO>>;
}
