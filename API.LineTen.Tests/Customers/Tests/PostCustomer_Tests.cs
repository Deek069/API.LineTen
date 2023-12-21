using API.LineTen.Controllers;
using Application.LineTen.Customers.DTOs;
using Application.LineTen.Customers.Commands.CreateCustomer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;

namespace API.LineTen.Tests.Customers.Tests
{
    public class PostCustomer_Tests
    {
        private Mock<ILogger<CustomersController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private CustomersController _customersController;
        private CustomerTestData _customerTestData;

        public PostCustomer_Tests()
        {
            _mockLogger = new Mock<ILogger<CustomersController>>();
            _mockMediator = new Mock<IMediator>();
            _customersController = new CustomersController(_mockLogger.Object, _mockMediator.Object);
            _customerTestData = new CustomerTestData();
        }

        [Fact]
        public async Task PostCustomer_Should_CreateANewCustomer_And_ReturnTheNewCustomerDetails()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CustomerDTO.FromCustomer(_customerTestData.Customer1));
            var command = new CreateCustomerCommand(
                _customerTestData.Customer1.FirstName,
                _customerTestData.Customer1.LastName,
                _customerTestData.Customer1.Phone,
                _customerTestData.Customer1.Email
            );

            // Act
            var result = (ActionResult)await _customersController.CreateCustomer(command);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);

            var customer = (CustomerDTO)actionResult.Value;
            Assert.NotEqual(expected: Guid.Empty, actual: customer.ID);
            Assert.Equal(expected: _customerTestData.Customer1.FirstName, actual: customer.FirstName);
            Assert.Equal(expected: _customerTestData.Customer1.LastName, actual: customer.LastName);
            Assert.Equal(expected: _customerTestData.Customer1.Phone, actual: customer.Phone);
            Assert.Equal(expected: _customerTestData.Customer1.Email, actual: customer.Email);
        }
    }
}
