using System.Net;
using Domain.LineTen.Customers;

namespace LineTen.IntegrationTests.Customers.Tests
{
    public class DeleteCustomer_Test : IntegrationTest
    {
        [Fact]
        public async Task Delete_Should_ReturnOK_IfAValidIDIsProvided()
        {
            // Arrange
            var testData = new CustomerTestData();
            var methods = new CustomerMethods(TestClient);
            var newCustomer = await methods.CreateCustomer(testData.CreateCustomerCommand1);
            Assert.NotNull(newCustomer);

            // Act
            var deleteResponse = await TestClient.DeleteAsync($"Customers/{newCustomer.ID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: deleteResponse.StatusCode);
        }

        [Fact]
        public async Task Delete_Should_ReturnNotFound_IfAnInvalidIDIsProvided()
        {
            // Arrange
            var customerID = CustomerID.CreateUnique().value;

            // Act
            var deleteResponse = await TestClient.DeleteAsync($"Customers/{customerID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: deleteResponse.StatusCode);
        }
    }
}
