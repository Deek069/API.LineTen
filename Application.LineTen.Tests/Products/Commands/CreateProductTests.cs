using Moq;
using Application.LineTen.Products.Commands.CreateProduct;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Common.Interfaces;
using Domain.LineTen.Entities;
using Application.LineTen.Products.Exceptions;

namespace Application.LineTen.Tests.Products.Commands
{
    public class CreateProductTests
    {
        private readonly ProductsTestData _productsTestData;
        private readonly CreateProductCommandHandler _handler;
        private readonly Mock<IProductsRepository> _productsRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateProductTests()
        {
            _productsTestData = new ProductsTestData();
            _productsRepoMock = new Mock<IProductsRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateProductCommandHandler(_productsRepoMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Should_CreateProductAndReturnCreatedOrderDetails()
        {
            // Arrange
            var command = new CreateProductCommand(
                _productsTestData.Product1.Name,
                _productsTestData.Product1.Description,
                _productsTestData.Product1.SKU
            );

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            _productsRepoMock.Verify(repo => repo.Create(It.IsAny<Product>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotEqual(expected: Guid.Empty, actual: result.ID);
            Assert.Equal(expected: command.Name, actual: result.Name);
            Assert.Equal(expected: command.Description, actual: result.Description);
            Assert.Equal(expected: command.SKU, actual: result.SKU);
        }

        [Fact]
        public async Task Handler_Should_ThrowException_WhenInvalidDataProvided()
        {
            try
            {
                // Arrange
                var command = new CreateProductCommand("", "", "");

                // Act
                var result = await _handler.Handle(command, default);

                // Assert
                Assert.Fail("ProductValidationException not thrown");
            }
            catch (ProductValidationException px)
            {
                _productsRepoMock.Verify(repo => repo.Create(It.IsAny<Product>()), Times.Never);
                _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            }
            catch (Exception ex) 
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
