using Domain.LineTen.ValueObjects.Products;
using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Products
{
    public class ProductsRepository_ProductExists_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly ProductsRepository _repo;
        private readonly ProductsTestData _testData;

        public ProductsRepository_ProductExists_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new ProductsRepository(_db);
            _testData = new ProductsTestData();
        }

        [Fact]
        public async Task ProductExists_Should_ReturnTrue_ForValidID()
        {
            // Arrange
            _repo.Create(_testData.Product1);
            await _unitOfWork.SaveChangesAsync();

            // Act
            var result = _repo.ProductExists(_testData.Product1.ID);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ProductExists_Should_ReturnFalse_ForInvalidID()
        {
            // Arrange

            // Act
            var customerID = ProductID.CreateUnique();
            var result = _repo.ProductExists(customerID);

            // Assert
            Assert.False(result);
        }
    }
}
