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
            var command = new UpdateOrderCommand()
            {
                OrderID = newOrder.ID,
                Status = OrderStatus.Complete
            };

            // Act
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await TestClient.PutAsync("Orders", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
        }

        [Fact]
        public async Task PostOrder_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var command = new UpdateOrderCommand()
            {
                OrderID = OrderID.CreateUnique().value,
                Status = OrderStatus.Complete
            };

            // Act
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await TestClient.PutAsync("Orders", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
        }
    }
}
