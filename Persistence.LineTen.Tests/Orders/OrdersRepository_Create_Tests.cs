using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Orders
{
    public class OrdersRepository_Create_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly OrdersRepository _repo;
        private readonly OrdersTestData _testData;

        public OrdersRepository_Create_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new OrdersRepository(_db);
            _testData = new OrdersTestData();
        }

        [Fact]
        public async Task Create_Should_CreateAnOrder()
        {
            // Arrange
            await _testData.CreateTestCustomersAndProducts(_db, _unitOfWork);

            // Act
            var actionDate = DateTime.Now;
            _repo.Create(_testData.Order1);
            await _unitOfWork.SaveChangesAsync();
            var verifyOrder = _repo.GetById(_testData.Order1.ID);

            // Assert
            Assert.NotNull(verifyOrder);
            Assert.Equal(expected: _testData.Order1.ID.value, actual: verifyOrder.ID.value);
            Assert.Equal(expected: _testData.Order1.CustomerID.value, actual: verifyOrder.CustomerID.value);
            Assert.Equal(expected: _testData.Order1.ProductID.value, actual: verifyOrder.ProductID.value);
            Assert.Equal(expected: _testData.Order1.Status, actual: verifyOrder.Status);
            Assert.True(_testData.Order1.CreatedDate >= actionDate, "Created date was not updated.");
        }
    }
}
