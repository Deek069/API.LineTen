using Application.LineTen.Orders.DTOs;
using Application.LineTen.Orders.Exceptions;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Orders.Queries.GetOrderByID;
using Application.LineTen.Orders.Queries.GetOrderSummary;
using Domain.LineTen.Orders;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LineTen.Tests.Orders.Queries
{
    public class GetOrderSummaryTests
    {
        private readonly OrdersTestData _ordersTestData;
        private readonly GetOrderSummaryQueryHandler _handler;
        private readonly Mock<IOrdersRepository> _ordersRepoMock;

        public GetOrderSummaryTests()
        {
            _ordersTestData = new OrdersTestData();
            _ordersRepoMock = new Mock<IOrdersRepository>();
            _handler = new GetOrderSummaryQueryHandler(_ordersRepoMock.Object);
        }

        [Fact]
        public async Task Handler_Should_ReturnOrder_IfValidIDProvided()
        {
            try
            {
                // Arrange
                var expectedOrder = OrderSummaryDTO.FromOrder(_ordersTestData.Order1);
                _ordersRepoMock.Setup(repo => repo.GetById(_ordersTestData.Order1.ID)).Returns(_ordersTestData.Order1);

                // Act
                var query = new GetOrderSummaryQuery(_ordersTestData.Order1.ID.value);
                var result = await _handler.Handle(query, default);

                // Assert
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
                _ordersRepoMock.Setup(repo => repo.GetById(It.IsAny<OrderID>())).Returns(valueFunction: () => null);

                // Act
                var query = new GetOrderSummaryQuery(orderID.value);
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
