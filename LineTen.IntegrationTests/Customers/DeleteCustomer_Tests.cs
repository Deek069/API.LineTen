using Application.LineTen.Customers.DTOs;
using System.Net.Http.Json;
using System.Net;
using Domain.LineTen.Customers;

namespace LineTen.IntegrationTests.Customers
{
    public class DeleteCustomer_Test: IntegrationTest
    {
        [Fact]
        public async Task Delete_Should_ReturnOK_IfAValidIDIsProvided()
        {
            // Arrange
            var testData = new CustomerTestData();
            var methods = new CustomerMethods(TestClient);
            var postResponse = await methods.CreateCustomer(testData.Customer1);
            var newCustomer = await postResponse.Content.ReadFromJsonAsync<CustomerDTO>();

            // Act
            var deleteResponse = await methods.DeleteCustomer(newCustomer.ID);

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: deleteResponse.StatusCode);
        }

        [Fact]
        public async Task Delete_Should_ReturnNotFound_IfAnInvalidIDIsProvided()
        {
            // Arrange
            var methods = new CustomerMethods(TestClient);
            var customerID = CustomerID.CreateUnique().value;

            // Act
            var deleteResponse = await methods.DeleteCustomer(customerID);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: deleteResponse.StatusCode);
        }
    }
}
