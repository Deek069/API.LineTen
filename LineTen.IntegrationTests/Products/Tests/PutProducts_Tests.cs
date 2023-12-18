using System.Net;
using Domain.LineTen.Products;
using Application.LineTen.Products.Commands.UpdateProduct;
using System.Text;
using System.Text.Json;

namespace LineTen.IntegrationTests.Products.Tests
{
    public class PutProduct_Tests : IntegrationTest
    {
        [Fact]
        public async Task PostProduct_Should_ReturnOK_WithValidID()
        {
            // Arrange
            var testData = new ProductTestData();
            var methods = new ProductMethods(TestClient);
            var newProduct = await methods.CreateProduct(testData.CreateProductCommand1);
            Assert.NotNull(newProduct);

            var updateCommand = new UpdateProductCommand()
            {
                ProductID = newProduct.ID,
                Name = "Triumph Street Triple",
                Description = "The Triumph Street Triple is a naked or streetfighter motorcycle made by Triumph Motorcycles, first released towards the end of 2007. The bike is closely modelled on the Speed Triple 1050 but uses a re-tuned inline three cylinder 675 cc engine from the Daytona 675 sport bike, which was released in 2006.",
                SKU = "TRI-675R",
            };
            var jsonContent = JsonSerializer.Serialize(updateCommand);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync("Products", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
        }

        [Fact]
        public async Task PostProduct_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var updateCommand = new UpdateProductCommand()
            {
                ProductID = ProductID.CreateUnique().value,
                Name = "Triumph Street Triple",
                Description = "The Triumph Street Triple is a naked or streetfighter motorcycle made by Triumph Motorcycles, first released towards the end of 2007. The bike is closely modelled on the Speed Triple 1050 but uses a re-tuned inline three cylinder 675 cc engine from the Daytona 675 sport bike, which was released in 2006.",
                SKU = "TRI-675R",
            };
            var jsonContent = JsonSerializer.Serialize(updateCommand);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync("Products", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
        }
    }
}
