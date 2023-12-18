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
            var testData = new ProductsTestData();
            var methods = new ProductMethods(TestClient);

            var postResponse1 = await methods.CreateProduct(testData.Product1);
            var newProduct1 = await postResponse1.Content.ReadFromJsonAsync<ProductDTO>();

            var postResponse2 = await methods.CreateProduct(testData.Product2);
            var newProduct2 = await postResponse2.Content.ReadFromJsonAsync<ProductDTO>();

            // Act
            var getResponse = await methods.GetProducts();

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: getResponse.StatusCode);
            var Products = await getResponse.Content.ReadFromJsonAsync<List<ProductDTO>>();

            Assert.True(Products.Count > 2, "Less than the expected number of Products returned");
            Assert.True(Products.Where(m => m.ID == newProduct1.ID).Count() == 1, "Product 1 not returned");
            Assert.True(Products.Where(m => m.ID == newProduct2.ID).Count() == 1, "Product 2 not returned");
        }
    }
}
