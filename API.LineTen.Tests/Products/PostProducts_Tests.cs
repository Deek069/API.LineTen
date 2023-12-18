﻿using API.LineTen.Controllers;
using Application.LineTen.Products.DTOs;
using Application.LineTen.Products.Commands.CreateProduct;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;

namespace API.LineTen.Tests.Products
{
    public class PostProduct_Tests
    {
        private Mock<ILogger<ProductsController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private ProductsController _ProductsController;
        private ProductsTestData _ProductsTestData;

        public PostProduct_Tests()
        {
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _mockMediator = new Mock<IMediator>();
            _ProductsController = new ProductsController(_mockLogger.Object, _mockMediator.Object);
            _ProductsTestData = new ProductsTestData();
        }

        [Fact]
        public async Task PostProduct_Should_CreateANewProduct_And_ReturnTheNewProductDetails()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ProductDTO.FromProduct(_ProductsTestData.Product1));
            var command = new CreateProductCommand()
            {
                Name = _ProductsTestData.Product1.Name,
                Description = _ProductsTestData.Product1.Description,
                SKU = _ProductsTestData.Product1.SKU,
            };

            // Act
            var result = (ActionResult)await _ProductsController.CreateProduct(command);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);

            var Product = (ProductDTO)actionResult.Value;
            Assert.NotEqual(expected: Guid.Empty, actual: Product.ID);
            Assert.Equal(expected: _ProductsTestData.Product1.Name, actual: Product.Name);
            Assert.Equal(expected: _ProductsTestData.Product1.Description, actual: Product.Description);
            Assert.Equal(expected: _ProductsTestData.Product1.SKU, actual: Product.SKU);
        }
    }
}
