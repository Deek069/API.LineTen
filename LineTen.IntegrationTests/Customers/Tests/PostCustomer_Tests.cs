using Application.LineTen.Customers.DTOs;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace LineTen.IntegrationTests.Customers.Tests
{
    public class PostCustomer_Tests : IntegrationTest
    {
        [Fact]
        public async Task PostCustomer_Should_CreateANewCustomer_And_ReturnTheNewCustomerDetails()
        {
            // Arrange
            var testData = new CustomerTestData();
            var methods = new CustomerMethods(TestClient);

            // Act
            var jsonContent = JsonSerializer.Serialize(testData.CreateCustomerCommand1);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await TestClient.PostAsync("Customers", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.Created, actual: response.StatusCode);
            var newCustomer = await response.Content.ReadFromJsonAsync<CustomerDTO>();

            Assert.NotEqual(expected: Guid.Empty, actual: newCustomer.ID);
            Assert.Equal(expected: testData.CreateCustomerCommand1.FirstName, actual: newCustomer.FirstName);
            Assert.Equal(expected: testData.CreateCustomerCommand1.LastName, actual: newCustomer.LastName);
            Assert.Equal(expected: testData.CreateCustomerCommand1.Phone, actual: newCustomer.Phone);
            Assert.Equal(expected: testData.CreateCustomerCommand1.Email, actual: newCustomer.Email);
        }
    }
}
