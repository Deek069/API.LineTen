using Application.LineTen.Products.DTOs;
using System.Net.Http.Json;
using System.Net;
using Domain.LineTen.Products;

namespace LineTen.IntegrationTests.Products.Tests
{
    public class DeleteProduct_Test : IntegrationTest
    {
        [Fact]
        public async Task Delete_Should_ReturnOK_IfAValidIDIsProvided()
        {
            // Arrange
            var testData = new ProductsTestData();
            var methods = new ProductMethods(TestClient);
            var postResponse = await methods.CreateProduct(testData.Product1);
            var newProduct = await postResponse.Content.ReadFromJsonAsync<ProductDTO>();

            // Act
            var deleteResponse = await methods.DeleteProduct(newProduct.ID);

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
            var deleteResponse = await methods.DeleteProduct(productID);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: deleteResponse.StatusCode);
        }
    }
}
