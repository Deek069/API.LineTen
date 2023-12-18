using Moq;
using Domain.LineTen.Orders;
using Application.LineTen.Orders.Commands.CreateOrder;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Common.Interfaces;
using Application.LineTen.Customers.Commands.DeleteCustomer;

namespace Application.LineTen.Tests.Orders.Commands
{
    public class CreateOrderTests
    {
        private readonly OrdersTestData _ordersTestData;
        private readonly CreateOrderCommandHandler _handler;
        private readonly Mock<IOrdersRepository> _ordersRepoMock;
        private readonly Mock<ICustomersRepository> _customersRepoMock;
        private readonly Mock<IProductsRepository> _productsRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateOrderTests()
        {
            _ordersTestData = new OrdersTestData();
            _ordersRepoMock = new Mock<IOrdersRepository>();
            _customersRepoMock = new Mock<ICustomersRepository>();
            _productsRepoMock = new Mock<IProductsRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateOrderCommandHandler(_ordersRepoMock.Object,
                                                     _customersRepoMock.Object,
                                                     _productsRepoMock.Object,
                                                     _unitOfWorkMock.Object);
        }


        [Fact]
        public async Task Handler_Should_CreateOrderAndReturnCreatedOrderDetails()
        {
            // Arrange
            _customersRepoMock.Setup(repo => repo.GetById(_ordersTestData.CustomerTestData.Customer1.ID)).Returns(_ordersTestData.CustomerTestData.Customer1);
            _customersRepoMock.Setup(repo => repo.CustomerExists(_ordersTestData.CustomerTestData.Customer1.ID)).Returns(true);

            _productsRepoMock.Setup(repo => repo.GetById(_ordersTestData.ProductTestData.Product1.ID)).Returns(_ordersTestData.ProductTestData.Product1);
            _productsRepoMock.Setup(repo => repo.ProductExists(_ordersTestData.ProductTestData.Product1.ID)).Returns(true);

            // Act
            var command = new CreateOrderCommand()
            {
                CustomerID = _ordersTestData.CustomerTestData.Customer1.ID,
                ProductID = _ordersTestData.ProductTestData.Product1.ID
            };
            var result = await _handler.Handle(command, default);

            // Assert
            _ordersRepoMock.Verify(repo => repo.Create(It.IsAny<Order>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotEqual(expected: Guid.Empty, actual: result.ID);
            Assert.Equal(expected: command.CustomerID.value, actual: result.CustomerID);
            Assert.Equal(expected: command.ProductID.value, actual: result.ProductID);
        }
    }
}
