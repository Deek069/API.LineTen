using Application.LineTen.Products.DTOs;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace LineTen.IntegrationTests.Products.Tests
{
    public class PostProduct_Tests : IntegrationTest
    {
        [Fact]
        public async Task PostProduct_Should_CreateANewProduct_And_ReturnTheNewProductDetails()
        {
            // Arrange
            var testData = new ProductTestData();
            var methods = new ProductMethods(TestClient);

            // Act
            var jsonContent = JsonSerializer.Serialize(testData.CreateProductCommand1);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await TestClient.PostAsync("Products", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.Created, actual: response.StatusCode);
            var newProduct = await response.Content.ReadFromJsonAsync<ProductDTO>();

            Assert.NotEqual(expected: Guid.Empty, actual: newProduct.ID);
            Assert.Equal(expected: testData.CreateProductCommand1.Name, actual: newProduct.Name);
            Assert.Equal(expected: testData.CreateProductCommand1.Description, actual: newProduct.Description);
            Assert.Equal(expected: testData.CreateProductCommand1.SKU, actual: newProduct.SKU);
        }
    }
}
