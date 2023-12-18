using Application.LineTen.Products.DTOs;
using System.Net.Http.Json;
using System.Net;

namespace LineTen.IntegrationTests.Products.Tests
{
    public class GetProducts_Tests : IntegrationTest
    {
        [Fact]
        public async Task GetProducts_Should_ReturnAllProducts()
        {
            // Arrange
            var testData = new ProductTestData();
            var methods = new ProductMethods(TestClient);
            var newProduct1 = await methods.CreateProduct(testData.CreateProductCommand1);
            Assert.NotNull(newProduct1);
            var newProduct2 = await methods.CreateProduct(testData.CreateProductCommand2);
            Assert.NotNull(newProduct2);

            // Act
            var response = await TestClient.GetAsync($"Products");

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
            var Products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();

            Assert.True(Products.Count > 2, "Less than the expected number of Products returned");
            Assert.True(Products.Where(m => m.ID == newProduct1.ID).Count() == 1, "Product 1 not returned");
            Assert.True(Products.Where(m => m.ID == newProduct2.ID).Count() == 1, "Product 2 not returned");
        }
    }
}
