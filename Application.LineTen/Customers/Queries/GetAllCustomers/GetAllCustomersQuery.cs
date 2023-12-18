using Application.LineTen.Customers.DTOs;
using MediatR;

namespace Application.LineTen.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDTO>>
    {
    }
}
