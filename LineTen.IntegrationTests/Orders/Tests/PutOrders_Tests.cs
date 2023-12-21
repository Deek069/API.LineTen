using System.Net;
using Domain.LineTen.Orders;
using System.Text;
using Application.LineTen.Orders.Commands.UpdateOrder;
using System.Text.Json;

namespace LineTen.IntegrationTests.Orders.Tests
{
    public class PutOrder_Tests : IntegrationTest
    {
        [Fact]
        public async Task PostOrder_Should_ReturnOK_WithValidID()
        {
            // Arrange
            var testData = new OrdersTestData();
            await testData.CreateTestDataAsync(TestClient);
            
            var methods = new OrderMethods(TestClient);
            var newOrder = await methods.CreateOrder(testData.CreateOrderCommand1);
            var orderID = newOrder.ID;
            var request = new UpdateOrderRequest(OrderStatus.Complete);

            // Act
            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await TestClient.PutAsync($"Orders/{orderID}", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
        }

        [Fact]
        public async Task PostOrder_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var orderID = OrderID.CreateUnique().value;
            var request = new UpdateOrderRequest(OrderStatus.Complete);

            // Act
            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await TestClient.PutAsync($"Orders/{orderID}", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
        }
    }
}
