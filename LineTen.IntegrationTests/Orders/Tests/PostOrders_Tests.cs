using Application.LineTen.Orders.DTOs;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using Application.LineTen.Orders.Commands.CreateOrder;
using Domain.LineTen.ValueObjects.Customers;
using Domain.LineTen.ValueObjects.Products;

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

        [Fact]
        public async Task PostOrder_Should_ReturnNotFound_WhenInvalidCustomerIDProvided()
        {
            // Arrange
            var testData = new OrdersTestData();
            await testData.CreateTestDataAsync(TestClient);

            // Act
            var command = new CreateOrderCommand(CustomerID.CreateUnique().value, testData.CreateOrderCommand1.ProductID);
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await TestClient.PostAsync("Orders", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
        }

        [Fact]
        public async Task PostOrder_Should_ReturnNotFound_WhenInvalidProductIDProvided()
        {
            // Arrange
            var testData = new OrdersTestData();
            await testData.CreateTestDataAsync(TestClient);

            // Act
            var command = new CreateOrderCommand(testData.CreateOrderCommand1.CustomerID, ProductID.CreateUnique().value);
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await TestClient.PostAsync("Orders", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
        }
    }
}
