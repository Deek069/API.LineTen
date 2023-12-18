using Domain.LineTen.Customers;

namespace Domain.LineTen.Tests.Customers
{
    public class CustomerIDTests
    {
        [Fact]
        public void ProductID_Should_GenerateANewGUID()
        {
            // Arrange

            // Act
            var customerID = CustomerID.CreateUnique();

            // Assert
            Assert.NotEqual(expected: Guid.Empty, actual: customerID.value);
        }
    }
}
