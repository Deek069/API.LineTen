using Application.LineTen.Common.Interfaces;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Customers.Exceptions;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Orders.DTOs;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Products.Exceptions;
using Domain.LineTen.Entities;
using Domain.LineTen.ValueObjects.Customers;
using Domain.LineTen.ValueObjects.Products;

using MediatR;
using Domain.LineTen.ValueObjects.Orders;
using Domain.LineTen.Validation;
using Application.LineTen.Orders.Exceptions;
using Application.LineTen.Common;

namespace Application.LineTen.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDTO>
    {
        private IOrdersRepository _ordersRepository;
        private ICustomersRepository _customersRepository;
        private IProductsRepository _productsRepository;
        private IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IOrdersRepository ordersRepository, 
                                         ICustomersRepository customersRepository, 
                                         IProductsRepository productsRepository, 
                                         IUnitOfWork unitOfWork) 
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
            _productsRepository = productsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customerID = new CustomerID(request.CustomerID);
            var productID = new ProductID(request.ProductID);

            if (!_customersRepository.CustomerExists(customerID))
            {
                throw new CustomerNotFoundException(customerID);
            }
            if (!_productsRepository.ProductExists(productID))
            {
                throw new ProductNotFoundException(productID);
            }

            var order = new Order()
            {
                ID = OrderID.CreateUnique(),
                CustomerID = customerID,
                ProductID = productID,
                Status = OrderStatus.Pending,
            };

            var validator = new OrderValidator();
            var result = validator.Validate(order);
            if (!result.IsValid)
            {
                throw new OrderValidationException(ValidationExceptionMessage.Message(result.Errors));
            }

            _ordersRepository.Create(order);
            await _unitOfWork.SaveChangesAsync();
            return OrderDTO.FromOrder(order);
        }
    }
}
