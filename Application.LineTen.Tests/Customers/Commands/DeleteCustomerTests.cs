using Moq;
using Domain.LineTen.Customers;
using Application.LineTen.Customers.Commands.DeleteCustomer;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Common.Interfaces;

namespace Application.LineTen.Tests.Customers.Commands
{
    public class DeleteCustomerTests
    {
        private readonly CustomerTestData _customerTestData;
        private readonly DeleteCustomerCommandHandler _handler;
        private readonly Mock<ICustomersRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteCustomerTests()
        {
            _customerTestData = new CustomerTestData();
            _repositoryMock = new Mock<ICustomersRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new DeleteCustomerCommandHandler(_repositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Should_DeleteCustomerAndReturnTrue_IfValidIDProvided()
        {
            // Arrange
            var command = new DeleteCustomerCommand
            {
                ID = CustomerID.CreateUnique().value
            };
            _repositoryMock.Setup(repo => repo.GetById(new CustomerID(command.ID))).Returns(_customerTestData.Customer1);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<Customer>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Handler_Should_ReturnFalse_IfInvalidIDProvided()
        {
            // Arrange
            var command = new DeleteCustomerCommand
            {
                ID = CustomerID.CreateUnique().value
            };
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<CustomerID>())).Returns(valueFunction: () => null);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<Customer>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            Assert.False(result);
        }
    }
}
