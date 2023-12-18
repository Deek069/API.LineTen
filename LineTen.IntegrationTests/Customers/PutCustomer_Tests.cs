using Application.LineTen.Customers.DTOs;
using System.Net.Http.Json;
using System.Net;
using Domain.LineTen.Customers;

namespace LineTen.IntegrationTests.Customers
{
    public class PutCustomer_Tests : IntegrationTest
    {
        [Fact]
        public async Task PostCustomer_Should_ReturnOK_WithValidID()
        {
            // Arrange
            var testData = new CustomerTestData();
            var methods = new CustomerMethods(TestClient);
            var postResponse = await methods.CreateCustomer(testData.Customer1);
            var newCustomer = await postResponse.Content.ReadFromJsonAsync<CustomerDTO>();
            testData.Customer1.ID = new CustomerID(newCustomer.ID);

            // Act
            testData.Customer1.FirstName = "Jonathan";
            var putResponse = await methods.UpdateCustomer(testData.Customer1);

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: putResponse.StatusCode);
        }

        [Fact]
        public async Task PostCustomer_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var testData = new CustomerTestData();
            var methods = new CustomerMethods(TestClient);

            // Act
            var putResponse = await methods.UpdateCustomer(testData.Customer1);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: putResponse.StatusCode);
        }
    }
}
