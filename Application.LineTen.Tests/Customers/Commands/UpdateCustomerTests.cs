using Moq;
using Application.LineTen.Customers.Commands.UpdateCustomer;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Common.Interfaces;
using Application.LineTen.Customers.Exceptions;
using Domain.LineTen.Entities;
using Domain.LineTen.ValueObjects.Customers;
using Application.LineTen.Customers.Commands.CreateCustomer;

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
        public async Task Handler_Should_UpdateCustomer_IfValidIDProvided()
        {
            try
            {
                // Arrange
                var command = new UpdateCustomerCommand(
                    _customerTestData.Customer1.ID.value,
                    _customerTestData.Customer1.FirstName,
                    _customerTestData.Customer1.LastName,
                    _customerTestData.Customer1.Phone,
                    _customerTestData.Customer1.Email
                );
                _repositoryMock.Setup(repo => repo.GetById(_customerTestData.Customer1.ID)).Returns(_customerTestData.Customer1);

                // Act
                await _handler.Handle(command, default);

                // Assert
                _repositoryMock.Verify(repo => repo.Update(It.IsAny<Customer>()), Times.Once);
                _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occured: {ex.Message}");
            }
        }

        [Fact]
        public async Task Handler_Should_ThrowAnException_IfInvalidIDProvided()
        {
            try
            {
                // Arrange
                var command = new UpdateCustomerCommand(
                    _customerTestData.Customer1.ID.value,
                    _customerTestData.Customer1.FirstName,
                    _customerTestData.Customer1.LastName,
                    _customerTestData.Customer1.Phone,
                    _customerTestData.Customer1.Email
                );
                _repositoryMock.Setup(repo => repo.GetById(It.IsAny<CustomerID>())).Returns(valueFunction: () => null);

                // Act
                await _handler.Handle(command, default);

                // Assert
                Assert.Fail("CustomerNotFoundException was not thrown.");
            }
            catch (CustomerNotFoundException ex)
            {
                _repositoryMock.Verify(repo => repo.Update(It.IsAny<Customer>()), Times.Never);
                _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occured: {ex.Message}");
            }
        }

        [Fact]
        public async Task Handler_Should_ThrowException_WithInvalidData()
        {
            try
            {
                // Arrange
                _repositoryMock.Setup(repo => repo.GetById(_customerTestData.Customer1.ID)).Returns(_customerTestData.Customer1);
                var command = new UpdateCustomerCommand(_customerTestData.Customer1.ID.value, "", "", "", "");

                // Act
                await _handler.Handle(command, default);

                // Assert
                Assert.Fail("CustomerValidationException was not thrown.");
            }
            catch (CustomerValidationException cx)
            {
                _repositoryMock.Verify(repo => repo.Update(It.IsAny<Customer>()), Times.Never);
                _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occured: {ex.Message}");
            }
        }
    }
}
