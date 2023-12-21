using API.LineTen.Controllers;
using Application.LineTen.Orders.Commands.DeleteOrder;
using Domain.LineTen.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using Application.LineTen.Orders.Exceptions;

namespace API.LineTen.Tests.Orders.Tests
{
    public class DeleteOrder_Test
    {
        private Mock<ILogger<OrdersController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private OrdersController _OrdersController;
        private OrdersTestData _OrdersTestData;

        public DeleteOrder_Test()
        {
            _mockLogger = new Mock<ILogger<OrdersController>>();
            _mockMediator = new Mock<IMediator>();
            _OrdersController = new OrdersController(_mockLogger.Object, _mockMediator.Object);
            _OrdersTestData = new OrdersTestData();
        }

        [Fact]
        public async Task Delete_Should_ReturnOK_IfAValidIDIsProvided()
        {
            // Arrange
            var orderID = _OrdersTestData.Order1.ID;

            // Act
            var result = (ActionResult)await _OrdersController.DeleteOrder(orderID.value);

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_Should_ReturnNotFound_IfAnInvalidIDIsProvided()
        {
            // Arrange
            var orderID = OrderID.CreateUnique();
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteOrderCommand>(), It.IsAny<CancellationToken>()))
                        .Throws(new OrderNotFoundException(orderID));

            // Act
            var result = (ActionResult)await _OrdersController.DeleteOrder(orderID.value);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
