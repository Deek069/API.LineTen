using Application.LineTen.Orders.DTOs;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace LineTen.IntegrationTests.Orders.Tests
{
    public class PostOrder_Tests : IntegrationTest
    {
        [Fact]
        public async Task PostOrder_Should_CreateANewOrder_And_ReturnTheNewOrderDetails()
        {
            // Arrange
            var testData = new OrdersTestData();
            await testData.CreateTestDataAsync(TestClient);

            // Act
            var jsonContent = JsonSerializer.Serialize(testData.CreateOrderCommand1);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await TestClient.PostAsync("Orders", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.Created, actual: response.StatusCode);
            var newOrder = await response.Content.ReadFromJsonAsync<OrderDTO>();

            Assert.NotEqual(expected: Guid.Empty, actual: newOrder.ID);
            Assert.Equal(expected: testData.CreateOrderCommand1.CustomerID, actual: newOrder.CustomerID);
            Assert.Equal(expected: testData.CreateOrderCommand1.ProductID, actual: newOrder.ProductID);
        }
    }
}
