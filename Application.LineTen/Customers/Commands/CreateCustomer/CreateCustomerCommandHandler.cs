using Application.LineTen.Common;
using Application.LineTen.Common.Interfaces;
using Application.LineTen.Customers.DTOs;
using Application.LineTen.Customers.Exceptions;
using Application.LineTen.Customers.Interfaces;
using Domain.LineTen.Entities;
using Domain.LineTen.Validation;
using Domain.LineTen.ValueObjects.Customers;
using MediatR;

namespace Application.LineTen.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDTO>
    {
        private ICustomersRepository _customersRepository;
        private IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomersRepository customersRepository, IUnitOfWork unitOfWork)
        {
            _customersRepository = customersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDTO> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer()
            {
                ID = CustomerID.CreateUnique(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email
            };

            var validator = new CustomerValidator();
            var result = validator.Validate(customer);
            if (!result.IsValid)
            {
                throw new CustomerValidationException(ValidationExceptionMessage.Message(result.Errors));
            }

            _customersRepository.Create(customer);
            await _unitOfWork.SaveChangesAsync();
            return CustomerDTO.FromCustomer(customer);
        }
    }
}
