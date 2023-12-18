using Domain.LineTen.Customers;
using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Customers
{
    public class CustomersRepository_CustomerExists_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly CustomersRepository _repo;
        private readonly CustomerTestData _customerTestData;

        public CustomersRepository_CustomerExists_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new CustomersRepository(_db);
            _customerTestData = new CustomerTestData();
        }

        [Fact]
        public async Task CustomerExists_Should_ReturnTrue_ForValidID()
        {
            using (var context = TestDBContext.GetTestDBContext())
            {
                // Arrange

                // Act
                _repo.Create(_customerTestData.Customer1);
                await _unitOfWork.SaveChangesAsync();
                var result = _repo.CustomerExists(_customerTestData.Customer1.ID);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public async Task CustomerExists_Should_ReturnFalse_ForInvalidID()
        {
            // Act
            var customerID = CustomerID.CreateUnique();
            var result = _repo.CustomerExists(customerID);

            // Assert
            Assert.False(result);
        }
    }
}
