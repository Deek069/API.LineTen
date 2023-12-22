using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Products
{
    public class ProductsRepository_Delete_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly ProductsRepository _repo;
        private readonly ProductsTestData _testData;

        public ProductsRepository_Delete_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new ProductsRepository(_db);
            _testData = new ProductsTestData();
        }

        [Fact]
        public async Task Delete_Should_DeleteAProduct()
        {
            // Arrange
            _repo.Create(_testData.Product1);
            await _unitOfWork.SaveChangesAsync();

            // Act
            _repo.Delete(_testData.Product1);
            await _unitOfWork.SaveChangesAsync();
            var verifyProduct = _repo.GetById(_testData.Product1.ID);

            // Assert
            Assert.True(verifyProduct == null);
        }
    }
}
