using Application.LineTen.Products.DTOs;
using System.Net.Http.Json;
using System.Net;

namespace LineTen.IntegrationTests.Products.Tests
{
    public class PostProduct_Tests : IntegrationTest
    {
        [Fact]
        public async Task PostProduct_Should_CreateANewProduct_And_ReturnTheNewProductDetails()
        {
            // Arrange
            var testData = new ProductsTestData();
            var methods = new ProductMethods(TestClient);

            // Act
            var postResponse = await methods.CreateProduct(testData.Product1);

            // Assert
            Assert.Equal(expected: HttpStatusCode.Created, actual: postResponse.StatusCode);
            var newProduct = await postResponse.Content.ReadFromJsonAsync<ProductDTO>();

            Assert.NotEqual(expected: Guid.Empty, actual: newProduct.ID);
            Assert.Equal(expected: testData.Product1.Name, actual: newProduct.Name);
            Assert.Equal(expected: testData.Product1.Description, actual: newProduct.Description);
            Assert.Equal(expected: testData.Product1.SKU, actual: newProduct.SKU);
        }
    }
}
