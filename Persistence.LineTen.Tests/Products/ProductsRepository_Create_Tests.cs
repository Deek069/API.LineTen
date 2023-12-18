using Domain.LineTen.Products;
using Persistence.LineTen.Repositories;
using Persistence.LineTen.Tests.Orders;

namespace Persistence.LineTen.Tests.Products
{
    public class ProductsRepository_Create_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly ProductsRepository _repo;
        private readonly ProductsTestData _testData;

        public ProductsRepository_Create_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new ProductsRepository(_db);
            _testData = new ProductsTestData();
        }

        [Fact]
        public async Task Create_Should_CreateAProduct()
        {
            // Arrange

            // Act
            _repo.Create(_testData.Product1);
            await _unitOfWork.SaveChangesAsync();
            var verifyProduct = _repo.GetById(_testData.Product1.ID);

            // Assert
            Assert.Equal(expected: _testData.Product1.ID.value, actual: verifyProduct.ID.value);
            Assert.Equal(expected: _testData.Product1.Name, actual: verifyProduct.Name);
            Assert.Equal(expected: _testData.Product1.Description, actual: verifyProduct.Description);
            Assert.Equal(expected: _testData.Product1.SKU, actual: verifyProduct.SKU);
        }
    }
}
