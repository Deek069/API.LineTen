using Application.LineTen.Customers.DTOs;
using Application.LineTen.Customers.Interfaces;
using MediatR;

namespace Application.LineTen.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler: IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDTO>>
    {
        private ICustomersRepository _customersRepository;

        public GetAllCustomersQueryHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<IEnumerable<CustomerDTO>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var allCustomers = _customersRepository.GetAll();
            List<CustomerDTO> result = allCustomers.Select(CustomerDTO.FromCustomer).ToList();
            return result;
        }
    }
}
