using Moq;
using Domain.LineTen.Orders;
using Application.LineTen.Orders.Commands.UpdateOrder;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Common.Interfaces;

namespace Application.LineTen.Tests.Orders.Commands
{
    public class UpdateOrderTests
    {
        private readonly OrdersTestData _ordersTestData;
        private readonly UpdateOrderCommandHandler _handler;
        private readonly Mock<IOrdersRepository> _ordersRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateOrderTests()
        {
            _ordersTestData = new OrdersTestData();
            _ordersRepoMock = new Mock<IOrdersRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdateOrderCommandHandler(_ordersRepoMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Should_UpdateOrderAndReturnTrue_IfValidIDProvided()
        {
            // Arrange
            var command = new UpdateOrderCommand
            {
                ID = _ordersTestData.Order1.ID.value,
                Status = OrderStatus.Pending
            };

            _ordersRepoMock.Setup(repo => repo.GetById(_ordersTestData.Order1.ID)).Returns(_ordersTestData.Order1);

            // Act
            await _handler.Handle(command, default);

            // Assert
            _ordersRepoMock.Verify(repo => repo.Update(It.IsAny<Order>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_ReturnFalse_IfInvalidIDProvided()
        {
            // Arrange
            var orderID = OrderID.CreateUnique();
            var command = new UpdateOrderCommand
            {
                ID = orderID.value,
                Status = OrderStatus.Pending
            };
            _ordersRepoMock.Setup(repo => repo.GetById(It.IsAny<OrderID>())).Returns(valueFunction: () => null);

            // Act
            await _handler.Handle(command, default);

            // Assert
            _ordersRepoMock.Verify(repo => repo.Update(It.IsAny<Order>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
