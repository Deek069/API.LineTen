using Moq;
using Domain.LineTen.Products;
using Application.LineTen.Products.Commands.UpdateProduct;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Common.Interfaces;
using Application.LineTen.Products.Exceptions;

namespace Application.LineTen.Tests.Products.Commands
{
    public class UpdateProductTests
    {
        private readonly ProductsTestData _productsTestData;
        private readonly UpdateProductCommandHandler _handler;
        private readonly Mock<IProductsRepository> _productsRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateProductTests()
        {
            _productsTestData = new ProductsTestData();
            _productsRepoMock = new Mock<IProductsRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdateProductCommandHandler(_productsRepoMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Should_UpdateProduct_IfValidIDProvided()
        {
            try
            {
                // Arrange
                _productsRepoMock.Setup(repo => repo.GetById(_productsTestData.Product1.ID)).Returns(_productsTestData.Product1);
                _productsTestData.Product1.SKU = "KHI-201304";

                // Act
                var command = new UpdateProductCommand(
                    _productsTestData.Product1.ID.value,
                    _productsTestData.Product1.Name,
                    _productsTestData.Product1.Description,
                    _productsTestData.Product1.SKU
                );
                await _handler.Handle(command, default);

                // Assert
                _productsRepoMock.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once);
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
                var command = new UpdateProductCommand(
                    ProductID.CreateUnique().value,
                    _productsTestData.Product1.Name,
                    _productsTestData.Product1.Description,
                    _productsTestData.Product1.SKU
                );
                await _handler.Handle(command, default);

                // Assert
                Assert.Fail("ProductNotFoundException not thrown");
            }
            catch (ProductNotFoundException px)
            {
                _productsRepoMock.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Never);
                _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
