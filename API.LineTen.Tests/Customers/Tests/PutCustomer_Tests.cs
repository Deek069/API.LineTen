using API.LineTen.Controllers;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Customers.Commands.UpdateCustomer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using Domain.LineTen.Customers;

namespace API.LineTen.Tests.Customers.Tests
{
    public class PutCustomer_Tests
    {
        private Mock<ILogger<CustomersController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private Mock<ICustomersRepository> _mockRepository;
        private CustomersController _customersController;
        private CustomerTestData _customerTestData;

        public PutCustomer_Tests()
        {
            _mockLogger = new Mock<ILogger<CustomersController>>();
            _mockMediator = new Mock<IMediator>();
            _customersController = new CustomersController(_mockLogger.Object, _mockMediator.Object);
            _customerTestData = new CustomerTestData();
        }

        [Fact]
        public async Task PutCustomer_Should_ReturnOK_WithValidID()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            var command = new UpdateCustomerCommand()
            {
                ID = _customerTestData.Customer1.ID.value,
                FirstName = _customerTestData.Customer1.FirstName,
                LastName = _customerTestData.Customer1.LastName,
                Phone = _customerTestData.Customer1.Phone,
                Email = _customerTestData.Customer1.Email
            };

            // Act
            var result = (ActionResult)await _customersController.UpdateCustomer(command);

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutCustomer_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);
            var command = new UpdateCustomerCommand()
            {
                ID = CustomerID.CreateUnique().value,
                FirstName = "Marge",
                LastName = "Simpson",
                Phone = "01 02481 383848",
                Email = "marge.simpson@aol.com"
            };

            // Act
            var result = (ActionResult)await _customersController.UpdateCustomer(command);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
