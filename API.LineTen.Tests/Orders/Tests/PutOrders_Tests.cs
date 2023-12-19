using API.LineTen.Controllers;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Orders.Commands.UpdateOrder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using Domain.LineTen.Orders;

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
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateOrderCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            var command = new UpdateOrderCommand()
            {
                ID = _OrdersTestData.Order1.ID.value,
                Status = OrderStatus.Complete
            };

            // Act
            var result = (ActionResult)await _OrdersController.UpdateOrder(command);

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutOrder_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateOrderCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);
            var command = new UpdateOrderCommand()
            {
                ID = OrderID.CreateUnique().value,
                Status = OrderStatus.Complete
            };

            // Act
            var result = (ActionResult)await _OrdersController.UpdateOrder(command);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
