using Moq;
using Application.LineTen.Orders.Queries.GetOrderByID;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Orders.DTOs;
using Application.LineTen.Orders.Exceptions;
using Domain.LineTen.ValueObjects.Orders;

namespace Application.LineTen.Tests.Orders.Queries
{
    public class GetOrderByIDTests
    {
        private readonly OrdersTestData _ordersTestData;
        private readonly GetOrderByIDQueryHandler _handler;
        private readonly Mock<IOrdersRepository> _ordersRepoMock;

        public GetOrderByIDTests()
        {
            _ordersTestData = new OrdersTestData();
            _ordersRepoMock = new Mock<IOrdersRepository>();
            _handler = new GetOrderByIDQueryHandler(_ordersRepoMock.Object);
        }

        [Fact]
        public async Task Handler_Should_ReturnOrder_IfValidIDProvided()
        {
            try
            {
                // Arrange
                var query = new GetOrderByIDQuery(_ordersTestData.Order1.ID.value);
                _ordersRepoMock.Setup(repo => repo.GetById(_ordersTestData.Order1.ID)).Returns(_ordersTestData.Order1);

                // Act
                var result = await _handler.Handle(query, default);

                // Assert
                var expectedOrder = OrderDTO.FromOrder(_ordersTestData.Order1);
                Assert.Equal(expected: expectedOrder, actual: result);
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
                var query = new GetOrderByIDQuery(orderID.value);
                _ordersRepoMock.Setup(repo => repo.GetById(It.IsAny<OrderID>())).Returns(valueFunction: () => null);

                // Act
                var result = await _handler.Handle(query, default);

                // Assert
                Assert.Fail("OrderNotFoundException not thrown.");
            }
            catch (OrderNotFoundException ox)
            {

            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
