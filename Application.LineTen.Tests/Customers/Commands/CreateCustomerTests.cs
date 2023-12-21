using Moq;
using Domain.LineTen.Customers;
using Application.LineTen.Customers.Commands.CreateCustomer;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Common.Interfaces;

namespace Application.LineTen.Tests.Customers.Commands
{
    public class CreateCustomerTests
    {
        private readonly CustomerTestData _customerTestData;
        private readonly CreateCustomerCommandHandler _handler;
        private readonly Mock<ICustomersRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateCustomerTests()
        {
            _customerTestData = new CustomerTestData();
            _repositoryMock = new Mock<ICustomersRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateCustomerCommandHandler(_repositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Should_CreateCustomerAndReturnCreatedCustomerDetails()
        {
            // Arrange
            var command = new CreateCustomerCommand(
                _customerTestData.Customer1.FirstName,
                _customerTestData.Customer1.LastName,
                _customerTestData.Customer1.Phone,
                _customerTestData.Customer1.Email
            );

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            _repositoryMock.Verify(repo => repo.Create(It.IsAny<Customer>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotEqual(expected: Guid.Empty, actual: result.ID);
            Assert.Equal(expected: command.FirstName, actual: result.FirstName);
            Assert.Equal(expected: command.LastName, actual: result.LastName);
            Assert.Equal(expected: command.Phone, actual: result.Phone);
            Assert.Equal(expected: command.Email, actual: result.Email);
        }
    }
}
