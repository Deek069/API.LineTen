using Application.LineTen.Common.Interfaces;
using Application.LineTen.Customers.Interfaces;
using Domain.LineTen.Customers;
using MediatR;

namespace Application.LineTen.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomersRepository customersRepository, IUnitOfWork unitOfWork)
        {
            _customersRepository = customersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _customersRepository.GetById(new CustomerID(request.CustomerID));
            if (customer == null) return false;

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Phone = request.Phone;
            customer.Email = request.Email;
            _customersRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
