using Moq;
using Domain.LineTen.Customers;
using Application.LineTen.Customers.Commands.UpdateCustomer;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Common.Interfaces;

namespace Application.LineTen.Tests.Customers.Commands
{
    public class UpdateCustomerTests
    {
        private readonly CustomerTestData _customerTestData;
        private readonly UpdateCustomerCommandHandler _handler;
        private readonly Mock<ICustomersRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateCustomerTests()
        {
            _customerTestData = new CustomerTestData();
            _repositoryMock = new Mock<ICustomersRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdateCustomerCommandHandler(_repositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Should_UpdateCustomerAndReturnTrue_IfValidIDProvided()
        {
            // Arrange
            var command = new UpdateCustomerCommand
            {
                ID = _customerTestData.Customer1.ID.value,
                FirstName = _customerTestData.Customer1.FirstName,
                LastName = _customerTestData.Customer1.LastName,
                Phone = _customerTestData.Customer1.Phone,
                Email = _customerTestData.Customer1.Email
            };
            _repositoryMock.Setup(repo => repo.GetById(_customerTestData.Customer1.ID)).Returns(_customerTestData.Customer1);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            _repositoryMock.Verify(repo => repo.Update(It.IsAny<Customer>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Handler_Should_ReturnFalse_IfInvalidIDProvided()
        {
            // Arrange
            var command = new UpdateCustomerCommand
            {
                ID = _customerTestData.Customer1.ID.value,
                FirstName = _customerTestData.Customer1.FirstName,
                LastName = _customerTestData.Customer1.LastName,
                Phone = _customerTestData.Customer1.Phone,
                Email = _customerTestData.Customer1.Email
            };
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<CustomerID>())).Returns(valueFunction: () => null);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            _repositoryMock.Verify(repo => repo.Update(It.IsAny<Customer>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            Assert.False(result);
        }
    }
}
