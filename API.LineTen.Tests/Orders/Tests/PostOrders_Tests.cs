using API.LineTen.Controllers;
using Application.LineTen.Orders.DTOs;
using Application.LineTen.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using Application.LineTen.Customers.Exceptions;
using Domain.LineTen.ValueObjects.Customers;
using Domain.LineTen.ValueObjects.Products;
using Application.LineTen.Products.Exceptions;

namespace API.LineTen.Tests.Orders.Tests
{
    public class PostOrder_Tests
    {
        private Mock<ILogger<OrdersController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private OrdersController _OrdersController;
        private OrdersTestData _OrdersTestData;

        public PostOrder_Tests()
        {
            _mockLogger = new Mock<ILogger<OrdersController>>();
            _mockMediator = new Mock<IMediator>();
            _OrdersController = new OrdersController(_mockLogger.Object, _mockMediator.Object);
            _OrdersTestData = new OrdersTestData();
        }

        [Fact]
        public async Task PostOrder_Should_CreateANewOrder_And_ReturnTheNewOrderDetails()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(OrderDTO.FromOrder(_OrdersTestData.Order1));

            var customer = _OrdersTestData.Order1.Customer;
            var product = _OrdersTestData.Order1.Product;
            var command = new CreateOrderCommand(
                customer.ID.value,
                product.ID.value
            );

            // Act
            var result = (ActionResult)await _OrdersController.CreateOrder(command);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);

            var Order = (OrderDTO)actionResult.Value;
            Assert.NotEqual(expected: Guid.Empty, actual: Order.ID);
            Assert.Equal(expected: customer.ID.value, actual: Order.CustomerID);
            Assert.Equal(expected: product.ID.value, actual: Order.ProductID);
        }

        [Fact]
        public async Task PostOrder_Should_ReturnNotFound_WhenInvalidCustomerIDProvided()
        {
            // Arrange
            var customerID = CustomerID.CreateUnique();
            _mockMediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
                        .Throws(new CustomerNotFoundException(customerID));

            var product = _OrdersTestData.Order1.Product;
            var command = new CreateOrderCommand(
                customerID.value,
                product.ID.value
            );

            // Act
            var result = (ActionResult)await _OrdersController.CreateOrder(command);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PostOrder_Should_ReturnNotFound_WhenInvalidProductIDProvided()
        {
            // Arrange
            var productID = ProductID.CreateUnique();
            _mockMediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
                        .Throws(new ProductNotFoundException(productID));

            var customer = _OrdersTestData.Order1.Customer;
            var command = new CreateOrderCommand(
                customer.ID.value,
                productID.value
            );

            // Act
            var result = (ActionResult)await _OrdersController.CreateOrder(command);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
