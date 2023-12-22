using System.Net;
using Domain.LineTen.ValueObjects.Products;

namespace LineTen.IntegrationTests.Products.Tests
{
    public class DeleteProduct_Test : IntegrationTest
    {
        [Fact]
        public async Task Delete_Should_ReturnOK_IfAValidIDIsProvided()
        {
            // Arrange
            var testData = new ProductTestData();
            var methods = new ProductMethods(TestClient);
            var newProduct = await methods.CreateProduct(testData.CreateProductCommand1);
            Assert.NotNull(newProduct);

            // Act
            var deleteResponse = await TestClient.DeleteAsync($"Products/{newProduct.ID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: deleteResponse.StatusCode);
        }

        [Fact]
        public async Task Delete_Should_ReturnNotFound_IfAnInvalidIDIsProvided()
        {
            // Arrange
            var methods = new ProductMethods(TestClient);
            var productID = ProductID.CreateUnique().value;

            // Act
            var deleteResponse = await TestClient.DeleteAsync($"Products/{productID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: deleteResponse.StatusCode);
        }
    }
}
