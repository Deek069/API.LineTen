using Application.LineTen.Customers.DTOs;
using Application.LineTen.Customers.Exceptions;
using Application.LineTen.Customers.Interfaces;
using MediatR;

namespace Application.LineTen.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQueryHandler : IRequestHandler<GetCustomerByIDQuery, CustomerDTO>
    {
        private ICustomersRepository _customersRepository;

        public GetCustomerByIDQueryHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<CustomerDTO> Handle(GetCustomerByIDQuery request, CancellationToken cancellationToken)
        {
            var customer = _customersRepository.GetById(request.ID);
            if (customer == null) throw new CustomerNotFoundException(request.ID);
            var result = CustomerDTO.FromCustomer(customer);
            return result;
        }
    }
}
