using Domain.LineTen.ValueObjects.Products;

namespace Domain.LineTen.Tests.Products
{
    public class ProductIDTests
    {
        [Fact]
        public void ProductID_Should_GenerateANewGUID()
        {
            // Arrange

            // Act
            var productID = ProductID.CreateUnique();

            // Assert
            Assert.NotEqual(expected: Guid.Empty, actual: productID.value);
        }
    }
}
