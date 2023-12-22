using Moq;
using Application.LineTen.Customers.Queries.GetAllCustomers;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Customers.DTOs;
using Domain.LineTen.Entities;

namespace Application.LineTen.Tests.Customers.Queries
{
    public class GetAllCustomersTests
    {
        CustomerTestData _customerTestData;
        Mock<ICustomersRepository> _customersRepoMock;

        public GetAllCustomersTests()
        {
            _customerTestData = new CustomerTestData();
            _customersRepoMock = new Mock<ICustomersRepository>();
        }

        [Fact]
        public async Task Handler_Should_ReturnListOfAllCustomers()
        {
            // Arrange
            var query = new GetAllCustomersQuery();

            var handler = new GetAllCustomersQueryHandler(_customersRepoMock.Object);


            var allCustomers = new List<Customer> { _customerTestData.Customer1, _customerTestData.Customer2 };
            _customersRepoMock.Setup(repo => repo.GetAll()).Returns(allCustomers);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            var expectedResult = new List<CustomerDTO> { CustomerDTO.FromCustomer(_customerTestData.Customer1),
                                                         CustomerDTO.FromCustomer(_customerTestData.Customer2) };
            Assert.Equal(expected: expectedResult, actual: result);

        }
    }
}
