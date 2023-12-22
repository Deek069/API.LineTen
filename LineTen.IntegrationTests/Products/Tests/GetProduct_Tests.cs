using Application.LineTen.Products.DTOs;
using Domain.LineTen.ValueObjects.Products;
using System.Net;
using System.Net.Http.Json;

namespace LineTen.IntegrationTests.Products.Tests
{
    public class GetProduct_Tests : IntegrationTest
    {
        [Fact]
        public async Task GetProduct_Should_ReturnProduct_WithValidID()
        {
            // Arrange
            var testData = new ProductTestData();
            var methods = new ProductMethods(TestClient);
            var newProduct = await methods.CreateProduct(testData.CreateProductCommand1);
            Assert.NotNull(newProduct);

            // Act
            var getResponse = await TestClient.GetAsync($"Products/{newProduct.ID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: getResponse.StatusCode);
            var ProductGot = await getResponse.Content.ReadFromJsonAsync<ProductDTO>();

            Assert.Equal(expected: newProduct, actual: ProductGot);
        }

        [Fact]
        public async Task GetProduct_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var productID = ProductID.CreateUnique().value;

            // Act
            var getResponse = await TestClient.GetAsync($"Products/{productID}");

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: getResponse.StatusCode);
        }
    }
}
