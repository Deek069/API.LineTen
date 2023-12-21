using API.LineTen.Controllers;
using Application.LineTen.Customers.Commands.DeleteCustomer;
using Domain.LineTen.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using Application.LineTen.Customers.Exceptions;

namespace API.LineTen.Tests.Customers.Tests
{
    public class DeleteCustomer_Test
    {
        private Mock<ILogger<CustomersController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private CustomersController _customersController;
        private CustomerTestData _customerTestData;

        public DeleteCustomer_Test()
        {
            _mockLogger = new Mock<ILogger<CustomersController>>();
            _mockMediator = new Mock<IMediator>();
            _customersController = new CustomersController(_mockLogger.Object, _mockMediator.Object);
            _customerTestData = new CustomerTestData();
        }

        [Fact]
        public async Task Delete_Should_ReturnOK_IfAValidIDIsProvided()
        {
            // Arrange
            var customerID = _customerTestData.Customer1.ID;

            // Act
            var result = (ActionResult)await _customersController.DeleteCustomer(_customerTestData.Customer1.ID.value);

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_Should_ReturnNotFound_IfAnInvalidIDIsProvided()
        {
            // Arrange
            var customerID = CustomerID.CreateUnique();
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteCustomerCommand>(), It.IsAny<CancellationToken>()))
                        .Throws(new CustomerNotFoundException(customerID));

            // Act
            var result = (ActionResult)await _customersController.DeleteCustomer(customerID.value);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
