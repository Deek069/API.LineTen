using Domain.LineTen.Products;
using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Products
{
    public class ProductsRepository_GetAll_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly ProductsRepository _repo;
        private readonly ProductsTestData _testData;

        public ProductsRepository_GetAll_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new ProductsRepository(_db);
            _testData = new ProductsTestData();
        }

        [Fact]
        public async Task GetAll_Should_ReturnAllProducts()
        {
            // Arrange
            _repo.Create(_testData.Product1);
            _repo.Create(_testData.Product2);
            await _unitOfWork.SaveChangesAsync();

            // Act
            var allProducts = _repo.GetAll();

            // Assert
            Assert.True(allProducts.Count() >= 2, "Less products than expeceted returned.");
            Assert.True(allProducts.Where(m => m.ID.value == _testData.Product1.ID.value).Count() == 1, "Product 1 not returned.");
            Assert.True(allProducts.Where(m => m.ID.value == _testData.Product2.ID.value).Count() == 1, "Product 2 not returned.");
        }
    }
}
