using Application.LineTen.Products.DTOs;
using System.Net.Http.Json;
using System.Net;
using Domain.LineTen.Products;

namespace LineTen.IntegrationTests.Products.Tests
{
    public class PutProduct_Tests : IntegrationTest
    {
        [Fact]
        public async Task PostProduct_Should_ReturnOK_WithValidID()
        {
            // Arrange
            var testData = new ProductsTestData();
            var methods = new ProductMethods(TestClient);
            var postResponse = await methods.CreateProduct(testData.Product1);
            var newProduct = await postResponse.Content.ReadFromJsonAsync<ProductDTO>();
            testData.Product1.ID = new ProductID(newProduct.ID);

            // Act
            testData.Product1.SKU = "ABC-123456";
            var putResponse = await methods.UpdateProduct(testData.Product1);

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: putResponse.StatusCode);
        }

        [Fact]
        public async Task PostProduct_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var testData = new ProductsTestData();
            var methods = new ProductMethods(TestClient);

            // Act
            var putResponse = await methods.UpdateProduct(testData.Product1);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: putResponse.StatusCode);
        }
    }
}
