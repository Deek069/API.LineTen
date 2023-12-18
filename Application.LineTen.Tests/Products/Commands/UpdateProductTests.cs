﻿using Moq;
using Domain.LineTen.Products;
using Application.LineTen.Products.Commands.UpdateProduct;
using Application.LineTen.Products.Interfaces;
using Application.LineTen.Common.Interfaces;

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
        public async Task Handler_Should_UpdateProductAndReturnTrue_IfValidIDProvided()
        {
            // Arrange
            _productsRepoMock.Setup(repo => repo.GetById(_productsTestData.Product1.ID)).Returns(_productsTestData.Product1);

            _productsTestData.Product1.SKU = "KHI-201304";
            var command = new UpdateProductCommand
            {
                ProductID = _productsTestData.Product1.ID,
                Name = _productsTestData.Product1.Name,
                Description = _productsTestData.Product1.Description,
                SKU = _productsTestData.Product1.SKU
            };

            // Act
            await _handler.Handle(command, default);

            // Assert
            _productsRepoMock.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_ReturnFalse_IfInvalidIDProvided()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                ProductID = ProductID.CreateUnique(),
                Name = _productsTestData.Product1.Name,
                Description = _productsTestData.Product1.Description,
                SKU = _productsTestData.Product1.SKU
            };
            _productsRepoMock.Setup(repo => repo.GetById(It.IsAny<ProductID>())).Returns(valueFunction: () => null);

            // Act
            await _handler.Handle(command, default);

            // Assert
            _productsRepoMock.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
