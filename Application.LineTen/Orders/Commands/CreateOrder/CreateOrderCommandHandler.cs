using Domain.LineTen.Orders;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Orders.DTOs;
using Application.LineTen.Common.Interfaces;
using MediatR;

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
            if (!_customersRepository.CustomerExists(request.CustomerID))
            {
                throw new ArgumentException("Customer does not exist.");
            }
            if (!_productsRepository.ProductExists(request.ProductID))
            {
                throw new ArgumentException("Product does not exist.");
            }

            var order = new Order()
            {
                ID = OrderID.CreateUnique(),
                CustomerID = request.CustomerID,
                ProductID = request.ProductID,
                Status = OrderStatus.Pending,
            };
            _ordersRepository.Create(order);
            await _unitOfWork.SaveChangesAsync();
            return OrderDTO.FromOrder(order);
        }
    }
}
