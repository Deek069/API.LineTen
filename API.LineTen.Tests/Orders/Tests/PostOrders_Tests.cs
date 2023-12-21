using API.LineTen.Controllers;
using Application.LineTen.Orders.DTOs;
using Application.LineTen.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;

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
    }
}
