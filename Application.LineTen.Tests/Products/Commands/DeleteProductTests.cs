using Moq;
using Domain.LineTen.Products;
using Application.LineTen.Products.Commands.DeleteProduct;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Common.Interfaces;
using Application.LineTen.Products.Exceptions;

namespace Application.LineTen.Tests.Products.Commands
{
    public class DeleteProductTests
    {
        private readonly ProductsTestData _productsTestData;
        private readonly DeleteProductCommandHandler _handler;
        private readonly Mock<IProductsRepository> _productsRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteProductTests()
        {
            _productsTestData = new ProductsTestData();
            _productsRepoMock = new Mock<IProductsRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new DeleteProductCommandHandler(_productsRepoMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Should_DeleteProduct_IfValidIDProvided()
        {
            try
            {
                // Arrange
                var productID = ProductID.CreateUnique();
                _productsRepoMock.Setup(repo => repo.GetById(_productsTestData.Product1.ID)).Returns(_productsTestData.Product1);

                // Act
                var command = new DeleteProductCommand(_productsTestData.Product1.ID.value);
                await _handler.Handle(command, default);

                // Assert
                _productsRepoMock.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Once);
                _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [Fact]
        public async Task Handler_Should_ThrowNotFound_IfInvalidIDProvided()
        {
            try
            {
                // Arrange
                _productsRepoMock.Setup(repo => repo.GetById(It.IsAny<ProductID>())).Returns(valueFunction: () => null);

                // Act
                var command = new DeleteProductCommand(ProductID.CreateUnique().value);
                await _handler.Handle(command, default);

                // Assert
                Assert.Fail("ProductNotFoundException not thrown");
            }
            catch (ProductNotFoundException px)
            {
                _productsRepoMock.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Never);
                _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
