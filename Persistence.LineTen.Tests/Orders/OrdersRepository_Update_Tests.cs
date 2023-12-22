using Domain.LineTen.ValueObjects.Orders;
using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Orders
{
    public class OrdersRepository_Update_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly OrdersRepository _repo;
        private readonly OrdersTestData _testData;

        public OrdersRepository_Update_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new OrdersRepository(_db);
            _testData = new OrdersTestData();
        }

        [Fact]
        public async Task Update_Should_UpdateTheStatusOfOrder()
        {
            // Arrange
            await _testData.CreateTestCustomersAndProducts(_db, _unitOfWork);
            _repo.Create(_testData.Order1);
            await _unitOfWork.SaveChangesAsync();

            // Act
            var actionDate = DateTime.Now;
            _testData.Order1.Status = OrderStatus.Complete;
            _repo.Update(_testData.Order1);
            await _unitOfWork.SaveChangesAsync();

            var verifyOrder = _repo.GetById(_testData.Order1.ID);

            // Assert
            Assert.Equal(expected: _testData.Order1.ID.value, actual: verifyOrder.ID.value);
            Assert.Equal(expected: _testData.Order1.CustomerID.value, actual: verifyOrder.CustomerID.value);
            Assert.Equal(expected: _testData.Order1.ProductID.value, actual: verifyOrder.ProductID.value);
            Assert.Equal(expected: _testData.Order1.Status, actual: verifyOrder.Status);
            Assert.True(_testData.Order1.UpdatedDate >= actionDate, "Updated date was not updated.");
        }
    }
}
