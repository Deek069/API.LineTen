﻿using System.Net;
using Application.LineTen.Products.Commands.UpdateProduct;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Domain.LineTen.ValueObjects.Products;

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

            var productID = newProduct.ID;
            var request = new UpdateProductRequest(
                "Triumph Street Triple",
                "The Triumph Street Triple is a naked or streetfighter motorcycle made by Triumph Motorcycles, first released towards the end of 2007. The bike is closely modelled on the Speed Triple 1050 but uses a re-tuned inline three cylinder 675 cc engine from the Daytona 675 sport bike, which was released in 2006.",
                "TRI-675R"
            );
            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync($"Products/{productID}", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
        }

        [Fact]
        public async Task PostProduct_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var productID = ProductID.CreateUnique().value;
            var request = new UpdateProductRequest(
                "Triumph Street Triple",
                "The Triumph Street Triple is a naked or streetfighter motorcycle made by Triumph Motorcycles, first released towards the end of 2007. The bike is closely modelled on the Speed Triple 1050 but uses a re-tuned inline three cylinder 675 cc engine from the Daytona 675 sport bike, which was released in 2006.",
                "TRI-675R"
            );
            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync($"Products/{productID}", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
        }

        [Fact]
        public async Task PostProduct_Should_ReturnBadRequest_WhenInvalidDataProvided()
        {
            // Arrange
            var testData = new ProductTestData();
            var methods = new ProductMethods(TestClient);
            var newProduct = await methods.CreateProduct(testData.CreateProductCommand1);
            Assert.NotNull(newProduct);

            var productID = newProduct.ID;
            var request = new UpdateProductRequest(
                "",
                "",
                ""
            );
            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync($"Products/{productID}", content);

            // Assert
            Assert.Equal(expected: HttpStatusCode.BadRequest, actual: response.StatusCode);
        }
    }
}
