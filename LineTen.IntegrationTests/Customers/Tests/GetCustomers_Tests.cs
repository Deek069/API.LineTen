using Application.LineTen.Customers.DTOs;
using System.Net.Http.Json;
using System.Net;

namespace LineTen.IntegrationTests.Customers.Tests
{
    public class GetCustomers_Tests : IntegrationTest
    {
        [Fact]
        public async Task GetCustomers_Should_ReturnAllCustomers()
        {
            // Arrange
            var testData = new CustomerTestData();
            var methods = new CustomerMethods(TestClient);
            var newCustomer1 = await methods.CreateCustomer(testData.CreateCustomerCommand1);
            Assert.NotNull(newCustomer1);
            var newCustomer2 = await methods.CreateCustomer(testData.CreateCustomerCommand2);
            Assert.NotNull(newCustomer2);

            // Act
            var response = await TestClient.GetAsync($"Customers");

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
            var customers = await response.Content.ReadFromJsonAsync<List<CustomerDTO>>();

            Assert.True(customers.Count > 2, "Less than the expected number of customers returned");
            Assert.True(customers.Where(m => m.ID == newCustomer1.ID).Count() == 1, "Customer 1 not returned");
            Assert.True(customers.Where(m => m.ID == newCustomer2.ID).Count() == 1, "Customer 2 not returned");
        }
    }
}
