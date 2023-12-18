using Moq;
using Domain.LineTen.Products;
using Application.LineTen.Products.Commands.DeleteProduct;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Common.Interfaces;

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
        public async Task Handler_Should_DeleteProductAndReturnTrue_IfValidIDProvided()
        {
            // Arrange
            var productID = ProductID.CreateUnique();
            var command = new DeleteProductCommand
            {
                ProductID = _productsTestData.Product1.ID
            };
            _productsRepoMock.Setup(repo => repo.GetById(command.ProductID)).Returns(_productsTestData.Product1);

            // Act
            await _handler.Handle(command, default);

            // Assert
            _productsRepoMock.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_ReturnFalse_IfInvalidIDProvided()
        {
            // Arrange
            var command = new DeleteProductCommand
            {
                ProductID = ProductID.CreateUnique()
            };

            _productsRepoMock.Setup(repo => repo.GetById(It.IsAny<ProductID>())).Returns(valueFunction: () => null);

            // Act
            await _handler.Handle(command, default);

            // Assert
            _productsRepoMock.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
