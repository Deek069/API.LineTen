using Moq;
using Application.LineTen.Products.Queries.GetProductByID;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Products.DTOs;
using Application.LineTen.Products.Exceptions;
using Domain.LineTen.ValueObjects.Products;

namespace Application.LineTen.Tests.Products.Queries
{
    public class GetProductByIDTests
    {
        private readonly ProductsTestData _productsTestData;
        private readonly GetProductByIDQueryHandler _handler;
        private readonly Mock<IProductsRepository> _productsRepoMock;

        public GetProductByIDTests()
        {
            _productsTestData = new ProductsTestData();
            _productsRepoMock = new Mock<IProductsRepository>();
            _handler = new GetProductByIDQueryHandler(_productsRepoMock.Object);
        }

        [Fact]
        public async Task Handler_Should_ReturnProduct_WithValidID()
        {
            try
            {
                // Arrange
                var query = new GetProductByIDQuery(_productsTestData.Product1.ID);
                _productsRepoMock.Setup(repo => repo.GetById(_productsTestData.Product1.ID)).Returns(_productsTestData.Product1);

                // Act
                var result = await _handler.Handle(query, default);

                // Assert
                var expectedProduct = ProductDTO.FromProduct(_productsTestData.Product1);
                Assert.Equal(expected: expectedProduct, actual: result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [Fact]
        public async Task Handler_Should_ThrowNotFound_WithInvalidID()
        {
            try
            {
                // Arrange
                var productID = ProductID.CreateUnique();
                var query = new GetProductByIDQuery(productID);

                _productsRepoMock.Setup(repo => repo.GetById(It.IsAny<ProductID>())).Returns(valueFunction: () => null);

                // Act
                var result = await _handler.Handle(query, default);

                // Assert
                Assert.Fail("ProductNotFoundException not thrown");
            }
            catch (ProductNotFoundException px)
            {

            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
