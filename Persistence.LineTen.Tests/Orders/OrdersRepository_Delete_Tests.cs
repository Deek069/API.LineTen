using Domain.LineTen.Customers;
using Domain.LineTen.Orders;
using Domain.LineTen.Products;
using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Orders
{
    public class OrdersRepository_Delete_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly OrdersRepository _repo;
        private readonly OrdersTestData _testData;

        public OrdersRepository_Delete_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new OrdersRepository(_db);
            _testData = new OrdersTestData();
        }

        [Fact]
        public async Task Delete_Should_DeleteAnOrder()
        {
            // Arrange
            await _testData.CreateTestCustomersAndProducts(_db, _unitOfWork);
            _repo.Create(_testData.Order1);
            await _unitOfWork.SaveChangesAsync();

            // Act
            _repo.Delete(_testData.Order1);
            await _unitOfWork.SaveChangesAsync();

            var verifyCustomer = _repo.GetById(_testData.Order1.ID);

            // Assert
            Assert.True(verifyCustomer == null);
        }
    }
}
