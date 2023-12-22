using API.LineTen.Controllers;
using Application.LineTen.Products.DTOs;
using Application.LineTen.Products.Queries.GetProductByID;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using Application.LineTen.Products.Exceptions;
using Domain.LineTen.ValueObjects.Products;

namespace API.LineTen.Tests.Products.Tests
{
    public class GetProduct_Tests
    {
        private Mock<ILogger<ProductsController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private ProductsController _ProductsController;
        private ProductsTestData _ProductsTestData;

        public GetProduct_Tests()
        {
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _mockMediator = new Mock<IMediator>();
            _ProductsController = new ProductsController(_mockLogger.Object, _mockMediator.Object);
            _ProductsTestData = new ProductsTestData();
        }

        [Fact]
        public async Task GetProduct_Should_ReturnProduct_WithValidID()
        {
            // Arrange
            var expectedProduct = ProductDTO.FromProduct(_ProductsTestData.Product1);
            _mockMediator.Setup(x => x.Send(It.IsAny<GetProductByIDQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedProduct);

            // Act
            var result = (ActionResult)await _ProductsController.GetByID(_ProductsTestData.Product1.ID.value);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var Product = (ProductDTO)actionResult.Value;
            Assert.Equal(expected: expectedProduct, actual: Product);
        }

        [Fact]
        public async Task GetProduct_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var productID = ProductID.CreateUnique();
            _mockMediator.Setup(x => x.Send(It.IsAny<GetProductByIDQuery>(), It.IsAny<CancellationToken>()))
                        .Throws(new ProductNotFoundException(productID));

            // Act
            var result = (ActionResult)await _ProductsController.GetByID(productID.value);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
