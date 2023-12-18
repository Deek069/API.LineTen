using Application.LineTen.Customers.DTOs;
using System.Net.Http.Json;
using System.Net;

namespace LineTen.IntegrationTests.Customers
{
    public class GetCustomers_Tests : IntegrationTest
    {
        [Fact]
        public async Task GetCustomers_Should_ReturnAllCustomers()
        {
            // Arrange
            var testData = new CustomerTestData();
            var methods = new CustomerMethods(TestClient);

            var postResponse1 = await methods.CreateCustomer(testData.Customer1);
            var newCustomer1 = await postResponse1.Content.ReadFromJsonAsync<CustomerDTO>();

            var postResponse2 = await methods.CreateCustomer(testData.Customer2);
            var newCustomer2 = await postResponse2.Content.ReadFromJsonAsync<CustomerDTO>();

            // Act
            var getResponse = await methods.GetCustomers();

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: getResponse.StatusCode);
            var customers = await getResponse.Content.ReadFromJsonAsync<List<CustomerDTO>>();

            Assert.True(customers.Count > 2, "Less than the expected number of customers returned");
            Assert.True(customers.Where(m => m.ID == newCustomer1.ID).Count() == 1, "Customer 1 not returned");
            Assert.True(customers.Where(m => m.ID == newCustomer2.ID).Count() == 1, "Customer 2 not returned");
        }
    }
}
