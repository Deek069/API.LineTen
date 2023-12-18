using API.LineTen.Controllers;
using Application.LineTen.Products.Commands.DeleteProduct;
using Domain.LineTen.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;

namespace API.LineTen.Tests.Products.Tests
{
    public class DeleteProduct_Test
    {
        private Mock<ILogger<ProductsController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private ProductsController _ProductsController;
        private ProductsTestData _ProductsTestData;

        public DeleteProduct_Test()
        {
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _mockMediator = new Mock<IMediator>();
            _ProductsController = new ProductsController(_mockLogger.Object, _mockMediator.Object);
            _ProductsTestData = new ProductsTestData();
        }

        [Fact]
        public async Task Delete_Should_ReturnOK_IfAValidIDIsProvided()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

            // Act
            var result = (ActionResult)await _ProductsController.DeleteProduct(_ProductsTestData.Product1.ID.value);

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_Should_ReturnNotFound_IfAnInvalidIDIsProvided()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);

            // Act
            var result = (ActionResult)await _ProductsController.DeleteProduct(ProductID.CreateUnique().value);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
