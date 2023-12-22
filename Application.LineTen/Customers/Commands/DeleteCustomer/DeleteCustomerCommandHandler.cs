using Application.LineTen.Common.Interfaces;
using Application.LineTen.Customers.Exceptions;
using Application.LineTen.Customers.Interfaces;
using Domain.LineTen.ValueObjects.Customers;
using MediatR;

namespace Application.LineTen.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private ICustomersRepository _customersRepository;
        private IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(ICustomersRepository customersRepository, IUnitOfWork unitOfWork)
        {
            _customersRepository = customersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerID = new CustomerID(request.ID);
            var customer = _customersRepository.GetById(customerID);
            if (customer == null) throw new CustomerNotFoundException(customerID);

            _customersRepository.Delete(customer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
