using API.LineTen.Controllers;
using Application.LineTen.Orders.Commands.DeleteOrder;
using Domain.LineTen.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;

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
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteOrderCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

            // Act
            var result = (ActionResult)await _OrdersController.DeleteOrder(_OrdersTestData.Order1.ID.value);

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_Should_ReturnNotFound_IfAnInvalidIDIsProvided()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteOrderCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);

            // Act
            var result = (ActionResult)await _OrdersController.DeleteOrder(OrderID.CreateUnique().value);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
