using API.LineTen.Controllers;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Products.Commands.UpdateProduct;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using Application.LineTen.Products.Exceptions;
using Domain.LineTen.ValueObjects.Products;

namespace API.LineTen.Tests.Products.Tests
{
    public class PutProduct_Tests
    {
        private Mock<ILogger<ProductsController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private Mock<IProductsRepository> _mockRepository;
        private ProductsController _ProductsController;
        private ProductsTestData _ProductsTestData;

        public PutProduct_Tests()
        {
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _mockMediator = new Mock<IMediator>();
            _ProductsController = new ProductsController(_mockLogger.Object, _mockMediator.Object);
            _ProductsTestData = new ProductsTestData();
        }

        [Fact]
        public async Task PutProduct_Should_ReturnOK_WithValidID()
        {
            // Arrange
            var productID = _ProductsTestData.Product1.ID.value;
            var request = new UpdateProductRequest(
                _ProductsTestData.Product1.Name,
                _ProductsTestData.Product1.Description,
                _ProductsTestData.Product1.SKU
            );

            // Act
            var result = (ActionResult)await _ProductsController.UpdateProduct(productID, request);

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutProduct_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var productID = ProductID.CreateUnique();
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
                        .Throws(new ProductNotFoundException(productID));
            var request = new UpdateProductRequest(
                "Triumph Street Triple",
                "The Triumph Street Triple is a naked or streetfighter motorcycle made by Triumph Motorcycles, first released towards the end of 2007. The bike is closely modelled on the Speed Triple 1050 but uses a re-tuned inline three cylinder 675 cc engine from the Daytona 675 sport bike, which was released in 2006.",
                "TRI-675R"
            );

            // Act
            var result = (ActionResult)await _ProductsController.UpdateProduct(productID.value, request);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PutProduct_Should_ReturnBadRequest_WhenInvalidDataProvided()
        {
            // Arrange
            var productID = _ProductsTestData.Product1.ID.value;
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
                        .Throws(new ProductValidationException("Invalid Data"));
            var request = new UpdateProductRequest("", "", "");

            // Act
            var result = (ActionResult)await _ProductsController.UpdateProduct(productID, request);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
