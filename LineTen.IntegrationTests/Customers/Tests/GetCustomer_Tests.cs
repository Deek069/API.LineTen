using Application.LineTen.Customers.Commands.CreateCustomer;
using Application.LineTen.Customers.DTOs;
using Domain.LineTen.Customers;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit.Sdk;

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

            var postResponse = await methods.CreateCustomer(testData.Customer1);
            var newCustomer = await postResponse.Content.ReadFromJsonAsync<CustomerDTO>();

            // Act
            var getResponse = await methods.GetCustomer(newCustomer.ID);

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
            var getResponse = await methods.GetCustomer(customerID);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: getResponse.StatusCode);
        }
    }
}
