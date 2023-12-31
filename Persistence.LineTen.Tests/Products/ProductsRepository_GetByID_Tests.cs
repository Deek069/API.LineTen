﻿using Domain.LineTen.ValueObjects.Products;
using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Products
{
    public class ProductsRepository_GetByID_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly ProductsRepository _repo;
        private readonly ProductsTestData _testData;

        public ProductsRepository_GetByID_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new ProductsRepository(_db);
            _testData = new ProductsTestData();
        }

        [Fact]
        public async Task GetByID_Should_ReturnTheProduct_ForValidID()
        {
            // Arrange
            _repo.Create(_testData.Product1);
            await _unitOfWork.SaveChangesAsync();

            // Act
            var verifyProduct = _repo.GetById(_testData.Product1.ID);

            // Assert
            Assert.Equal(expected: _testData.Product1.ID.value, actual: verifyProduct.ID.value);
            Assert.Equal(expected: _testData.Product1.Name, actual: verifyProduct.Name);
            Assert.Equal(expected: _testData.Product1.Description, actual: verifyProduct.Description);
            Assert.Equal(expected: _testData.Product1.SKU, actual: verifyProduct.SKU);
        }

        [Fact]
        public void GetByID_Should_ReturnNull_ForInvalidID()
        {
            // Arrange

            // Act
            var customerID = ProductID.CreateUnique();
            var verifyProduct = _repo.GetById(customerID);

            // Assert
            Assert.True(verifyProduct == null);
        }
    }
}
