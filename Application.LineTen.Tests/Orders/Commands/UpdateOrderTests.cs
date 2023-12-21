using Moq;
using Domain.LineTen.Orders;
using Application.LineTen.Orders.Commands.UpdateOrder;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Common.Interfaces;
using Application.LineTen.Orders.Exceptions;

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
        public async Task Handler_Should_UpdateOrder_IfValidIDProvided()
        {
            try
            {
                // Arrange
                var command = new UpdateOrderCommand(
                    _ordersTestData.Order1.ID.value,
                    OrderStatus.Pending
                );

                _ordersRepoMock.Setup(repo => repo.GetById(_ordersTestData.Order1.ID)).Returns(_ordersTestData.Order1);

                // Act
                await _handler.Handle(command, default);

                // Assert
                _ordersRepoMock.Verify(repo => repo.Update(It.IsAny<Order>()), Times.Once);
                _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [Fact]
        public async Task Handler_Should_ThrowNotFound_IfInvalidIDProvided()
        {
            try
            {
                // Arrange
                var orderID = OrderID.CreateUnique();
                var command = new UpdateOrderCommand(
                    orderID.value,
                    OrderStatus.Pending
                );
                _ordersRepoMock.Setup(repo => repo.GetById(It.IsAny<OrderID>())).Returns(valueFunction: () => null);

                // Act
                await _handler.Handle(command, default);

                // Assert
                Assert.Fail("OrderNotFoundException not throw.");
            }
            catch (OrderNotFoundException ox)
            {
                _ordersRepoMock.Verify(repo => repo.Update(It.IsAny<Order>()), Times.Never);
                _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
