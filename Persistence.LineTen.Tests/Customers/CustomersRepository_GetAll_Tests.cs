using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Customers
{
    public class CustomersRepository_GetAll_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly CustomersRepository _repo;
        private readonly CustomerTestData _customerTestData;

        public CustomersRepository_GetAll_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new CustomersRepository(_db);
            _customerTestData = new CustomerTestData();
        }

        [Fact]
        public async Task GetAll_Should_ReturnAllCustomers()
        {
            // Arrange
            _repo.Create(_customerTestData.Customer1);
            _repo.Create(_customerTestData.Customer2);
            await _unitOfWork.SaveChangesAsync();

            // Act
            var allCustomers = _repo.GetAll();

            // Assert
            Assert.True(allCustomers.Count() >= 2, "Less then the expected number of customers returned.");
            Assert.True(allCustomers.Where(m => m.ID.value == _customerTestData.Customer1.ID.value).Count() == 1, "Customer 1 not in the customers returned.");
            Assert.True(allCustomers.Where(m => m.ID.value == _customerTestData.Customer2.ID.value).Count() == 1, "Customer 2 not in the customers returned.");
        }
    }
}
