using Moq;
using Domain.LineTen.Orders;
using Application.LineTen.Orders.Commands.DeleteOrder;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Common.Interfaces;

namespace Application.LineTen.Tests.Orders.Commands
{
    public class DeleteOrderTests
    {
        private readonly OrdersTestData _ordersTestData;
        private readonly DeleteOrderCommandHandler _handler;
        private readonly Mock<IOrdersRepository> _ordersRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteOrderTests()
        {
            _ordersTestData = new OrdersTestData();
            _ordersRepoMock = new Mock<IOrdersRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new DeleteOrderCommandHandler(_ordersRepoMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Should_DeleteOrderAndReturnTrue_IfValidIDProvided()
        {
            // Arrange
            var command = new DeleteOrderCommand(_ordersTestData.Order1.ID.value);
            _ordersRepoMock.Setup(repo => repo.GetById(_ordersTestData.Order1.ID)).Returns(_ordersTestData.Order1);

            // Act
            await _handler.Handle(command, default);

            // Assert
            _ordersRepoMock.Verify(repo => repo.Delete(It.IsAny<Order>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_ReturnFalse_IfInvalidIDProvided()
        {
            // Arrange
            var command = new DeleteOrderCommand(OrderID.CreateUnique().value);
            _ordersRepoMock.Setup(repo => repo.GetById(It.IsAny<OrderID>())).Returns(valueFunction: () => null);

            // Act
            await _handler.Handle(command, default);

            // Assert
            _ordersRepoMock.Verify(repo => repo.Delete(It.IsAny<Order>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
