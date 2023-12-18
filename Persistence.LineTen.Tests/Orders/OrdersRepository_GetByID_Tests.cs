using Domain.LineTen.Customers;
using Domain.LineTen.Orders;
using Domain.LineTen.Products;
using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Orders
{
    public class OrdersRepository_GetByID_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly OrdersRepository _repo;
        private readonly OrdersTestData _testData;

        public OrdersRepository_GetByID_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new OrdersRepository(_db);
            _testData = new OrdersTestData();
        }

        [Fact]
        public async Task GetByID_Should_ReturnTheOrder_ForValidID()
        {
            // Arrange
            await _testData.CreateTestCustomersAndProducts(_db, _unitOfWork);
            _repo.Create(_testData.Order1);
            await _unitOfWork.SaveChangesAsync();

            // Act
            var verifyOrder = _repo.GetById(_testData.Order1.ID);

            // Assert
            Assert.Equal(expected: _testData.Order1.ID.value, actual: verifyOrder.ID.value);
            Assert.Equal(expected: _testData.Order1.CustomerID.value, actual: verifyOrder.CustomerID.value);
            Assert.Equal(expected: _testData.Order1.ProductID.value, actual: verifyOrder.ProductID.value);
            Assert.Equal(expected: _testData.Order1.Status, actual: verifyOrder.Status);
            Assert.Equal(expected: _testData.Order1.CreatedDate, actual: verifyOrder.CreatedDate);
            Assert.Equal(expected: _testData.Order1.UpdatedDate, actual: verifyOrder.UpdatedDate);

            Assert.Equal(expected: _testData.CustomerTestData.Customer1.FirstName, actual: verifyOrder.Customer.FirstName);
            Assert.Equal(expected: _testData.CustomerTestData.Customer1.LastName, actual: verifyOrder.Customer.LastName);
            Assert.Equal(expected: _testData.ProductTestData.Product1.Name, actual: verifyOrder.Product.Name);
            Assert.Equal(expected: _testData.ProductTestData.Product1.Description, actual: verifyOrder.Product.Description);
        }

        [Fact]
        public void GetByID_Should_ReturnNull_ForInvalidID()
        {
            using (var context = TestDBContext.GetTestDBContext())
            {
                // Arrange
                var repository = new OrdersRepository(context);

                // Act
                var orderID = OrderID.CreateUnique();
                var verifyOrder = repository.GetById(orderID);

                // Assert
                Assert.True(verifyOrder == null);
            }
        }
    }
}
