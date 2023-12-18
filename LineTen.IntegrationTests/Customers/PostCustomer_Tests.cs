using Application.LineTen.Customers.DTOs;
using System.Net.Http.Json;
using System.Net;

namespace LineTen.IntegrationTests.Customers
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
            var postResponse = await methods.CreateCustomer(testData.Customer1);

            // Assert
            Assert.Equal(expected: HttpStatusCode.Created, actual: postResponse.StatusCode);
            var newCustomer = await postResponse.Content.ReadFromJsonAsync<CustomerDTO>();

            Assert.NotEqual(expected: Guid.Empty, actual: newCustomer.ID);
            Assert.Equal(expected: testData.Customer1.FirstName, actual: newCustomer.FirstName);
            Assert.Equal(expected: testData.Customer1.LastName, actual: newCustomer.LastName);
            Assert.Equal(expected: testData.Customer1.Phone, actual: newCustomer.Phone);
            Assert.Equal(expected: testData.Customer1.Email, actual: newCustomer.Email);
        }
    }
}
