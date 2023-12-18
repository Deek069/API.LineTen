using Moq;
using Domain.LineTen.Customers;
using Application.LineTen.Customers.Queries.GetCustomerByID;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Customers.DTOs;

namespace Application.LineTen.Tests.Customers.Queries
{
    public class GetCustomerByIDTests
    {
        CustomerTestData _customerTestData;
        Mock<ICustomersRepository> _customersRepoMock;

        public GetCustomerByIDTests()
        {
            _customerTestData = new CustomerTestData();
            _customersRepoMock = new Mock<ICustomersRepository>();
        }

        [Fact]
        public async Task Handler_Should_ReturnCustomer_WithValidCustomerID()
        {
            // Arrange
            var handler = new GetCustomerByIDQueryHandler(_customersRepoMock.Object);

            var customerId = CustomerID.CreateUnique();
            var query = new GetCustomerByIDQuery { ID = customerId };

            _customersRepoMock.Setup(repo => repo.GetById(customerId)).Returns(_customerTestData.Customer1);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            var expectedCustomer = CustomerDTO.FromCustomer(_customerTestData.Customer1);
            Assert.Equal(expected: expectedCustomer, actual: result);
        }

        [Fact]
        public async Task Handler_Should_ReturnNull_WithInvalidCustomerID()
        {
            // Arrange
            var handler = new GetCustomerByIDQueryHandler(_customersRepoMock.Object);

            var customerId = CustomerID.CreateUnique();
            var query = new GetCustomerByIDQuery { ID = customerId };

            _customersRepoMock.Setup(repo => repo.GetById(It.IsAny<CustomerID>())).Returns(valueFunction: () => null);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.Null(result);
        }
    }
}
