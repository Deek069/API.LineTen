using Application.LineTen.Customers.DTOs;
using Domain.LineTen.Customers;
using System.Net;
using System.Net.Http.Json;

namespace LineTen.IntegrationTests.Customers.Tests
{
    public class GetCustomer_Tests : IntegrationTest
    {
        [Fact]
        public async Task GetCustomer_Should_ReturnCustomer_WithValidID()
        {
            // Arrange
            var testData = new CustomerTestData();
            var methods = new CustomerMethods(TestClient);
            var newCustomer = await methods.CreateCustomer(testData.CreateCustomerCommand1);
            Assert.NotNull(newCustomer);

            // Act
            var getResponse = await TestClient.GetAsync($"Customers/{newCustomer.ID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: getResponse.StatusCode);
            var customerGot = await getResponse.Content.ReadFromJsonAsync<CustomerDTO>();

            Assert.Equal(expected: newCustomer, actual: customerGot);
        }

        [Fact]
        public async Task GetCustomer_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var methods = new CustomerMethods(TestClient);
            var customerID = CustomerID.CreateUnique().value;

            // Act
            var getResponse = await TestClient.GetAsync($"Customers/{customerID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: getResponse.StatusCode);
        }
    }
}
