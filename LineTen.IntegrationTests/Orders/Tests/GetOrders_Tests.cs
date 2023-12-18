using Application.LineTen.Orders.DTOs;
using System.Net.Http.Json;
using System.Net;

namespace LineTen.IntegrationTests.Orders.Tests
{
    public class GetOrders_Tests : IntegrationTest
    {
        [Fact]
        public async Task GetOrders_Should_ReturnAllOrders()
        {
            // Arrange
            var testData = new OrdersTestData();
            await testData.CreateTestDataAsync(TestClient);

            var methods = new OrderMethods(TestClient);
            var newOrder1 = await methods.CreateOrder(testData.CreateOrderCommand1);
            var newOrder2 = await methods.CreateOrder(testData.CreateOrderCommand2);
            var newOrder3 = await methods.CreateOrder(testData.CreateOrderCommand3);
            var newOrder4 = await methods.CreateOrder(testData.CreateOrderCommand4);

            // Act
            var response = await TestClient.GetAsync($"Orders");

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
            var Orders = await response.Content.ReadFromJsonAsync<List<OrderDTO>>();

            Assert.True(Orders.Count >= 4, "Less than the expected number of Orders returned");
            Assert.True(Orders.Where(m => m.ID == newOrder1.ID).Count() == 1, "Order 1 not returned");
            Assert.True(Orders.Where(m => m.ID == newOrder2.ID).Count() == 1, "Order 2 not returned");
            Assert.True(Orders.Where(m => m.ID == newOrder3.ID).Count() == 1, "Order 3 not returned");
            Assert.True(Orders.Where(m => m.ID == newOrder4.ID).Count() == 1, "Order 4 not returned");
        }
    }
}
