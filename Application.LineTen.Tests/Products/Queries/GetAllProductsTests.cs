using Moq;
using Domain.LineTen.Products;
using Application.LineTen.Products.Queries.GetAllProducts;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Products.DTOs;

namespace Application.LineTen.Tests.Products.Queries
{
    public class GetAllProductsTests
    {
        private readonly ProductsTestData _productsTestData;
        private readonly GetAllProductsQueryHandler _handler;
        private readonly Mock<IProductsRepository> _productsRepoMock;

        public GetAllProductsTests()
        {
            _productsTestData = new ProductsTestData();
            _productsRepoMock = new Mock<IProductsRepository>();
            _handler = new GetAllProductsQueryHandler(_productsRepoMock.Object);
        }

        [Fact]
        public async Task Handler_Should_ReturnListOfAllProducts()
        {
            // Arrange
            var allProducts = new List<Product> { _productsTestData.Product1, _productsTestData.Product2 };
            _productsRepoMock.Setup(repo => repo.GetAll()).Returns(allProducts);

            // Act
            var query = new GetAllProductsQuery();
            var result = await _handler.Handle(query, default);

            // Assert
            var expectedResult = new List<ProductDTO> { ProductDTO.FromProduct(_productsTestData.Product1),
                                                        ProductDTO.FromProduct(_productsTestData.Product2) };
            Assert.Equal(expected: expectedResult, actual: result);
        }
    }
}
