using Moq;
using Application.LineTen.Orders.Commands.CreateOrder;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Common.Interfaces;
using Domain.LineTen.Entities;
using Domain.LineTen.ValueObjects.Customers;
using Application.LineTen.Customers.Exceptions;
using Domain.LineTen.ValueObjects.Products;
using Application.LineTen.Products.Exceptions;

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
            var customer = _ordersTestData.CustomerTestData.Customer1;
            _customersRepoMock.Setup(repo => repo.GetById(customer.ID)).Returns(customer);
            _customersRepoMock.Setup(repo => repo.CustomerExists(customer.ID)).Returns(true);

            var product = _ordersTestData.ProductTestData.Product1;
            _productsRepoMock.Setup(repo => repo.GetById(product.ID)).Returns(product);
            _productsRepoMock.Setup(repo => repo.ProductExists(product.ID)).Returns(true);

            // Act
            var command = new CreateOrderCommand(
                customer.ID.value,
                product.ID.value
            );
            var result = await _handler.Handle(command, default);

            // Assert
            _ordersRepoMock.Verify(repo => repo.Create(It.IsAny<Order>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotEqual(expected: Guid.Empty, actual: result.ID);
            Assert.Equal(expected: customer.ID.value, actual: result.CustomerID);
            Assert.Equal(expected: product.ID.value, actual: result.ProductID);
        }

        [Fact]
        public async Task Handler_Should_ThrowException_WhenInvalidCustomerIDProvided()
        {
            try
            {
                // Arrange
                var customerID = CustomerID.CreateUnique();
                _customersRepoMock.Setup(repo => repo.GetById(customerID)).Returns(valueFunction: () => null);
                _customersRepoMock.Setup(repo => repo.CustomerExists(customerID)).Returns(false);

                var product = _ordersTestData.ProductTestData.Product1;
                _productsRepoMock.Setup(repo => repo.GetById(product.ID)).Returns(product);
                _productsRepoMock.Setup(repo => repo.ProductExists(product.ID)).Returns(true);

                // Act
                var command = new CreateOrderCommand(
                    customerID.value,
                    product.ID.value
                );
                var result = await _handler.Handle(command, default);

                // Assert
                Assert.Fail("CustomerNotFoundException not thrown.");
            }
            catch (CustomerNotFoundException cx)
            {
                _ordersRepoMock.Verify(repo => repo.Create(It.IsAny<Order>()), Times.Never);
                _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [Fact]
        public async Task Handler_Should_ThrowException_WhenInvalidProductIDProvided()
        {
            try
            {
                // Arrange
                var customer = _ordersTestData.CustomerTestData.Customer1;
                _customersRepoMock.Setup(repo => repo.GetById(customer.ID)).Returns(customer);
                _customersRepoMock.Setup(repo => repo.CustomerExists(customer.ID)).Returns(true);

                var productID = ProductID.CreateUnique();
                _productsRepoMock.Setup(repo => repo.GetById(productID)).Returns(valueFunction: () => null);
                _productsRepoMock.Setup(repo => repo.ProductExists(productID)).Returns(false);

                // Act
                var command = new CreateOrderCommand(
                    customer.ID.value,
                    productID.value
                );
                var result = await _handler.Handle(command, default);

                // Assert
                Assert.Fail("ProductNotFoundException not thrown.");
            }
            catch (ProductNotFoundException px)
            {
                _ordersRepoMock.Verify(repo => repo.Create(It.IsAny<Order>()), Times.Never);
                _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
