using API.LineTen.Controllers;
using Application.LineTen.Orders.DTOs;
using Application.LineTen.Orders.Queries.GetAllOrders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;

namespace API.LineTen.Tests.Orders.Tests
{
    public class GetOrders_Tests
    {
        private Mock<ILogger<OrdersController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private OrdersController _OrdersController;
        private OrdersTestData _OrdersTestData;

        public GetOrders_Tests()
        {
            _mockLogger = new Mock<ILogger<OrdersController>>();
            _mockMediator = new Mock<IMediator>();
            _OrdersController = new OrdersController(_mockLogger.Object, _mockMediator.Object);
            _OrdersTestData = new OrdersTestData();
        }

        [Fact]
        public async Task GetOrders_Should_ReturnAllOrders()
        {
            // Arrange
            var expectedData = new List<OrderSummaryDTO> { OrderSummaryDTO.FromOrder(_OrdersTestData.Order1),
                                                           OrderSummaryDTO.FromOrder(_OrdersTestData.Order2),
                                                           OrderSummaryDTO.FromOrder(_OrdersTestData.Order3),
                                                           OrderSummaryDTO.FromOrder(_OrdersTestData.Order4) };
            _mockMediator.Setup(x => x.Send(It.IsAny<GetAllOrdersQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedData);

            // Act
            var result = (ActionResult)await _OrdersController.GetAll();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var Orders = (List<OrderSummaryDTO>)actionResult.Value;
            Assert.Equal(expected: expectedData.Count, actual: Orders.Count);
            Assert.Equal(expected: expectedData[0], actual: Orders[0]);
            Assert.Equal(expected: expectedData[1], actual: Orders[1]);
            Assert.Equal(expected: expectedData[2], actual: Orders[2]);
            Assert.Equal(expected: expectedData[3], actual: Orders[3]);
        }
    }
}
