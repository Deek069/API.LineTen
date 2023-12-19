using System.Net;
using Domain.LineTen.Customers;
using Application.LineTen.Customers.Commands.UpdateCustomer;
using System.Text;
using System.Text.Json;

namespace LineTen.IntegrationTests.Customers.Tests
{
    public class PutCustomer_Tests : IntegrationTest
    {
        [Fact]
        public async Task PostCustomer_Should_ReturnOK_WithValidID()
        {
            // Arrange
            var testData = new CustomerTestData();
            var methods = new CustomerMethods(TestClient);
            var newCustomer = await methods.CreateCustomer(testData.CreateCustomerCommand1);
            Assert.NotNull(newCustomer);

            var updateCommand = new UpdateCustomerCommand()
            {
                ID = newCustomer.ID,
                FirstName = "Jonathan",
                LastName = "Bobcat",
                Phone = "01293 48238389",
                Email = "jonny.bobcat@hotmail.com"
            };
            var jsonContent = JsonSerializer.Serialize(updateCommand);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync("Customers", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
        }

        [Fact]
        public async Task PostCustomer_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var updateCommand = new UpdateCustomerCommand()
            {
                ID = CustomerID.CreateUnique().value,
                FirstName = "Jonathan",
                LastName = "Bobcat",
                Phone = "01293 48238389",
                Email = "jonny.bobcat@hotmail.com"
            };
            var jsonContent = JsonSerializer.Serialize(updateCommand);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync("Customers", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
        }
    }
}
