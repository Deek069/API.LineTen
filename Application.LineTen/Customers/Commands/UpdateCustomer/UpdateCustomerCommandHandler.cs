using Application.LineTen.Common.Interfaces;
using Application.LineTen.Customers.Exceptions;
using Application.LineTen.Customers.Interfaces;
using Domain.LineTen.Customers;
using MediatR;

namespace Application.LineTen.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomersRepository customersRepository, IUnitOfWork unitOfWork)
        {
            _customersRepository = customersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerID = new CustomerID(request.ID);
            var customer = _customersRepository.GetById(customerID);
            if (customer == null) throw new CustomerNotFoundException(customerID);

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Phone = request.Phone;
            customer.Email = request.Email;
            _customersRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
