using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Orders
{
    public class OrdersRepository_GetAll_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly OrdersRepository _repo;
        private readonly OrdersTestData _testData;

        public OrdersRepository_GetAll_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new OrdersRepository(_db);
            _testData = new OrdersTestData();
        }

        [Fact]
        public async Task GetAll_Should_GetAllOrders()
        {
            // Arrange
            await _testData.CreateTestCustomersAndProducts(_db, _unitOfWork);
            _repo.Create(_testData.Order1);
            _repo.Create(_testData.Order2);
            _repo.Create(_testData.Order3);
            _repo.Create(_testData.Order4);
            await _unitOfWork.SaveChangesAsync();

            // Act
            var allOrders = _repo.GetAll();

            // Assert
            Assert.True(allOrders.Count() >= 4, "Less orders than expeceted returned.");
            Assert.True(allOrders.Where(m => m.ID.value == _testData.Order1.ID.value).Count() == 1, "Order 1 not returned.");
            Assert.True(allOrders.Where(m => m.ID.value == _testData.Order2.ID.value).Count() == 1, "Order 2 not returned.");
            Assert.True(allOrders.Where(m => m.ID.value == _testData.Order3.ID.value).Count() == 1, "Order 3 not returned.");
            Assert.True(allOrders.Where(m => m.ID.value == _testData.Order4.ID.value).Count() == 1, "Order 4 not returned.");
        }
    }
}
