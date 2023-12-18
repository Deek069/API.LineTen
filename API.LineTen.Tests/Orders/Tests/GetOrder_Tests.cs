﻿using API.LineTen.Controllers;
using Application.LineTen.Orders.DTOs;
using Domain.LineTen.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using Application.LineTen.Orders.Queries.GetOrderSummary;

namespace API.LineTen.Tests.Orders.Tests
{
    public class GetOrder_Tests
    {
        private Mock<ILogger<OrdersController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private OrdersController _OrdersController;
        private OrdersTestData _OrdersTestData;

        public GetOrder_Tests()
        {
            _mockLogger = new Mock<ILogger<OrdersController>>();
            _mockMediator = new Mock<IMediator>();
            _OrdersController = new OrdersController(_mockLogger.Object, _mockMediator.Object);
            _OrdersTestData = new OrdersTestData();
        }

        [Fact]
        public async Task GetOrder_Should_ReturnOrder_WithValidID()
        {
            // Arrange
            var expectedOrder = OrderSummaryDTO.FromOrder(_OrdersTestData.Order1);
            _mockMediator.Setup(x => x.Send(It.IsAny<GetOrderSummaryQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedOrder);

            // Act
            var result = (ActionResult)await _OrdersController.GetOrderSummary(_OrdersTestData.Order1.ID.value);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var Order = (OrderSummaryDTO)actionResult.Value;
            Assert.Equal(expected: expectedOrder, actual: Order);
        }

        [Fact]
        public async Task GetOrder_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<GetOrderSummaryQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(valueFunction: () => null);

            // Act
            var result = (ActionResult)await _OrdersController.GetOrderSummary(OrderID.CreateUnique().value);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
