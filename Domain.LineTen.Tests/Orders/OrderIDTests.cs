using Domain.LineTen.ValueObjects.Orders;

namespace Domain.LineTen.Tests.Orders
{
    public class OrderIDTests
    {
        [Fact]
        public void OrderID_Should_GenerateANewGUID()
        {
            // Arrange

            // Act
            var orderID = OrderID.CreateUnique();

            // Assert
            Assert.NotEqual(expected: Guid.Empty, actual: orderID.value);
        }
    }
}
