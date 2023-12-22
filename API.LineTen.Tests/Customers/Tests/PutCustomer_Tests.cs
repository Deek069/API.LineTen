using API.LineTen.Controllers;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Customers.Commands.UpdateCustomer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using Application.LineTen.Customers.Exceptions;
using Domain.LineTen.ValueObjects.Customers;
using Application.LineTen.Customers.Commands.CreateCustomer;

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
            var customerID = _customerTestData.Customer1.ID.value;
            var request = new UpdateCustomerRequest(
                _customerTestData.Customer1.FirstName,
                _customerTestData.Customer1.LastName,
                _customerTestData.Customer1.Phone,
                _customerTestData.Customer1.Email
            );

            // Act
            var result = (ActionResult)await _customersController.UpdateCustomer(customerID, request);

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutCustomer_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var customerID = CustomerID.CreateUnique();
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
                        .Throws(new CustomerNotFoundException(customerID));
            var request = new UpdateCustomerRequest(
                "Marge",
                "Simpson",
                "01 02481 383848",
                "marge.simpson@aol.com"
            );

            // Act
            var result = (ActionResult)await _customersController.UpdateCustomer(customerID.value, request);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PutCustomer_Should_ReturnBadRequest_WhenInvalidDataProvided()
        {
            // Arrange
            var customerID = _customerTestData.Customer1.ID.value;
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
                        .Throws(new CustomerValidationException("Invalid Data"));
            var request = new UpdateCustomerRequest("", "", "", "");

            // Act
            var result = (ActionResult)await _customersController.UpdateCustomer(customerID, request);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
