using Application.LineTen.Orders.DTOs;
using Domain.LineTen.ValueObjects.Orders;
using System.Net;
using System.Net.Http.Json;

namespace LineTen.IntegrationTests.Orders.Tests
{
    public class GetOrder_Tests : IntegrationTest
    {
        [Fact]
        public async Task GetOrder_Should_ReturnOrder_WithValidID()
        {
            // Arrange
            var testData = new OrdersTestData();
            await testData.CreateTestDataAsync(TestClient);

            var methods = new OrderMethods(TestClient);
            var newOrder = await methods.CreateOrder(testData.CreateOrderCommand1);
            Assert.NotNull(newOrder);

            // Act
            var response = await TestClient.GetAsync($"Orders/{newOrder.ID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
            var OrderGot = await response.Content.ReadFromJsonAsync<OrderSummaryDTO>();
            Assert.Equal(expected: newOrder.ID, actual: OrderGot.ID);
        }

        [Fact]
        public async Task GetOrder_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var orderID = OrderID.CreateUnique().value;

            // Act
            var response = await TestClient.GetAsync($"Orders/{orderID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
        }
    }
}
