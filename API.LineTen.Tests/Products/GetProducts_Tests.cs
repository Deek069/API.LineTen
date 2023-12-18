using API.LineTen.Controllers;
using Application.LineTen.Products.DTOs;
using Application.LineTen.Products.Queries.GetAllProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;

namespace API.LineTen.Tests.Products
{
    public class GetProducts_Tests
    {
        private Mock<ILogger<ProductsController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private ProductsController _ProductsController;
        private ProductsTestData _ProductsTestData;

        public GetProducts_Tests()
        {
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _mockMediator = new Mock<IMediator>();
            _ProductsController = new ProductsController(_mockLogger.Object, _mockMediator.Object);
            _ProductsTestData = new ProductsTestData();
        }

        [Fact]
        public async Task GetProducts_Should_ReturnAllProducts()
        {
            // Arrange
            var expectedData = new List<ProductDTO> { ProductDTO.FromProduct(_ProductsTestData.Product1),
                                                       ProductDTO.FromProduct(_ProductsTestData.Product2) };
            _mockMediator.Setup(x => x.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedData);

            // Act
            var result = (ActionResult)await _ProductsController.GetAll();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var Products = (List<ProductDTO>)actionResult.Value;
            Assert.Equal(expected: expectedData.Count, actual: Products.Count);
            Assert.Equal(expected: expectedData[0], actual: Products[0]);
            Assert.Equal(expected: expectedData[1], actual: Products[1]);
        }
    }
}
