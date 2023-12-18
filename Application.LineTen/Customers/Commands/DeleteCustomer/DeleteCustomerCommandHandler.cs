using Application.LineTen.Common.Interfaces;
using Application.LineTen.Customers.Interfaces;
using Domain.LineTen.Customers;
using MediatR;

namespace Application.LineTen.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private ICustomersRepository _customersRepository;
        private IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(ICustomersRepository customersRepository, IUnitOfWork unitOfWork)
        {
            _customersRepository = customersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _customersRepository.GetById(new CustomerID(request.CustomerID));
            if (customer == null) return false;

            _customersRepository.Delete(customer);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
