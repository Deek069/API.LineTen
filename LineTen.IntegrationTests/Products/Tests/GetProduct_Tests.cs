using Application.LineTen.Products.DTOs;
using Domain.LineTen.Products;
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
            var testData = new ProductsTestData();
            var methods = new ProductMethods(TestClient);

            var postResponse = await methods.CreateProduct(testData.Product1);
            var newProduct = await postResponse.Content.ReadFromJsonAsync<ProductDTO>();

            // Act
            var getResponse = await methods.GetProduct(newProduct.ID);

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: getResponse.StatusCode);
            var ProductGot = await getResponse.Content.ReadFromJsonAsync<ProductDTO>();

            Assert.Equal(expected: newProduct, actual: ProductGot);
        }

        [Fact]
        public async Task GetProduct_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var methods = new ProductMethods(TestClient);
            var productID = ProductID.CreateUnique().value;

            // Act
            var getResponse = await methods.GetProduct(productID);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: getResponse.StatusCode);
        }
    }
}
