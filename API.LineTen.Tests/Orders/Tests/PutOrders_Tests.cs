using API.LineTen.Controllers;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Orders.Commands.UpdateOrder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using Application.LineTen.Orders.Exceptions;
using Domain.LineTen.ValueObjects.Orders;

namespace API.LineTen.Tests.Orders.Tests
{
    public class PutOrder_Tests
    {
        private Mock<ILogger<OrdersController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private Mock<IOrdersRepository> _mockRepository;
        private OrdersController _OrdersController;
        private OrdersTestData _OrdersTestData;

        public PutOrder_Tests()
        {
            _mockLogger = new Mock<ILogger<OrdersController>>();
            _mockMediator = new Mock<IMediator>();
            _OrdersController = new OrdersController(_mockLogger.Object, _mockMediator.Object);
            _OrdersTestData = new OrdersTestData();
        }

        [Fact]
        public async Task PutOrder_Should_ReturnOK_WithValidID()
        {
            // Arrange
            var orderID = _OrdersTestData.Order1.ID.value;
            var request= new UpdateOrderRequest(OrderStatus.Complete);

            // Act
            var result = (ActionResult)await _OrdersController.UpdateOrder(orderID, request);

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutOrder_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var orderID = OrderID.CreateUnique();
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateOrderCommand>(), It.IsAny<CancellationToken>()))
                        .Throws(new OrderNotFoundException(orderID));
            var request = new UpdateOrderRequest(OrderStatus.Complete);

            // Act
            var result = (ActionResult)await _OrdersController.UpdateOrder(orderID.value, request);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PutOrder_Should_ReturnBadRequest_WhenInvalidDataProvided()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateOrderCommand>(), It.IsAny<CancellationToken>()))
                        .Throws(new OrderValidationException("Bad Data"));
            var orderID = _OrdersTestData.Order1.ID.value;
            var request = new UpdateOrderRequest(0);

            // Act
            var result = (ActionResult)await _OrdersController.UpdateOrder(orderID, request);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
