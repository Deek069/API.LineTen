using Application.LineTen.Orders.DTOs;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Orders.Queries.GetAllOrders;
using Domain.LineTen.Orders;
using Moq;

namespace Application.LineTen.Tests.Orders.Queries
{
    public class GetAllOrdersTests
    {
        private readonly OrdersTestData _ordersTestData;
        private readonly GetAllOrdersQueryHandler _handler;
        private readonly Mock<IOrdersRepository> _ordersRepoMock;

        public GetAllOrdersTests()
        {
            _ordersTestData = new OrdersTestData();
            _ordersRepoMock = new Mock<IOrdersRepository>();
            _handler = new GetAllOrdersQueryHandler(_ordersRepoMock.Object);
        }

        [Fact]
        public async Task Handler_Should_ReturnAllOrders()
        {
            // Arrange
            var allOrders = new List<Order> { _ordersTestData.Order1, _ordersTestData.Order2, _ordersTestData.Order3, _ordersTestData.Order4 };
            _ordersRepoMock.Setup(repo => repo.GetAll()).Returns(allOrders);

            // Act
            var query = new GetAllOrdersQuery();
            var result = await _handler.Handle(query, default);

            // Assert
            var expectedResult = new List<OrderDTO> { OrderDTO.FromOrder(_ordersTestData.Order1),
                                                      OrderDTO.FromOrder(_ordersTestData.Order2),
                                                      OrderDTO.FromOrder(_ordersTestData.Order3),
                                                      OrderDTO.FromOrder(_ordersTestData.Order4) };
            Assert.Equal(expected: expectedResult, actual: result);
        }
    }
}
