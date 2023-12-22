using Application.LineTen.Orders.DTOs;
using System.Net.Http.Json;
using System.Net;
using Domain.LineTen.ValueObjects.Orders;

namespace LineTen.IntegrationTests.Orders.Tests
{
    public class DeleteOrder_Test : IntegrationTest
    {
        [Fact]
        public async Task Delete_Should_ReturnOK_IfAValidIDIsProvided()
        {
            // Arrange
            var testData = new OrdersTestData();
            await testData.CreateTestDataAsync(TestClient);
            
            var methods = new OrderMethods(TestClient);
            var newOrder = await methods.CreateOrder(testData.CreateOrderCommand1);
            Assert.NotNull(newOrder);

            // Act
            var response = await TestClient.DeleteAsync($"Orders/{newOrder.ID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
        }

        [Fact]
        public async Task Delete_Should_ReturnNotFound_IfAnInvalidIDIsProvided()
        {
            // Arrange
            var orderID = OrderID.CreateUnique().value;

            // Act
            var response = await TestClient.DeleteAsync($"Orders/{orderID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
        }
    }
}
