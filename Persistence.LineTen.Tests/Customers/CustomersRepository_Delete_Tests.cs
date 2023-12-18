using Domain.LineTen.Customers;
using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Customers
{
    public class CustomersRepository_Delete_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly CustomersRepository _repo;
        private readonly CustomerTestData _customerTestData;

        public CustomersRepository_Delete_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new CustomersRepository(_db);
            _customerTestData = new CustomerTestData();
        }

        [Fact]
        public async Task Delete_Should_DeleteTheCustomer()
        {
            // Arrange
            _repo.Create(_customerTestData.Customer1);
            await _unitOfWork.SaveChangesAsync();

            // Act
            _repo.Delete(_customerTestData.Customer1);
            await _unitOfWork.SaveChangesAsync();
            var verifyCustomer = _repo.GetById(_customerTestData.Customer1.ID);

            // Assert
            Assert.True(verifyCustomer == null);
        }
    }
}
