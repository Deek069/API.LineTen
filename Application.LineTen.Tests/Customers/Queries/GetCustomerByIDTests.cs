using Moq;
using Domain.LineTen.Customers;
using Application.LineTen.Customers.Queries.GetCustomerByID;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Customers.DTOs;
using Application.LineTen.Customers.Exceptions;

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
            try
            {
                // Arrange
                var handler = new GetCustomerByIDQueryHandler(_customersRepoMock.Object);

                var customerId = CustomerID.CreateUnique();
                var query = new GetCustomerByIDQuery(customerId);
                _customersRepoMock.Setup(repo => repo.GetById(customerId)).Returns(_customerTestData.Customer1);

                // Act
                var result = await handler.Handle(query, default);
                
                // Assert
                var expectedCustomer = CustomerDTO.FromCustomer(_customerTestData.Customer1);
                Assert.Equal(expected: expectedCustomer, actual: result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [Fact]
        public async Task Handler_Should_ThrowNotFound_WithInvalidCustomerID()
        {
            try
            {
                // Arrange
                var handler = new GetCustomerByIDQueryHandler(_customersRepoMock.Object);

                var customerId = CustomerID.CreateUnique();
                var query = new GetCustomerByIDQuery(customerId);

                _customersRepoMock.Setup(repo => repo.GetById(It.IsAny<CustomerID>())).Returns(valueFunction: () => null);

                // Act
                var result = await handler.Handle(query, default);

                // Assert
                Assert.Fail("CustomerNotFoundException not thrown");
            }
            catch (CustomerNotFoundException e) 
            { 

            } catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
