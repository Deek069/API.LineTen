using API.LineTen.Controllers;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Customers.Commands.UpdateCustomer;
using Domain.LineTen.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;

namespace API.LineTen.Tests.Customers
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
        public async Task PostCustomer_Should_ReturnOK_WithValidID()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

            // Act
            var result = (ActionResult)await _customersController.UpdateCustomer(
                _customerTestData.Customer1.ID.value,
                _customerTestData.Customer1.FirstName,
                _customerTestData.Customer1.LastName,
                _customerTestData.Customer1.Phone,
                _customerTestData.Customer1.Email
            );

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PostCustomer_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);

            // Act
            var result = (ActionResult)await _customersController.UpdateCustomer(
                CustomerID.CreateUnique().value,
                "Marge",
                "Simpson",
                "01 02481 383848",
                "marge.simpson@aol.com"
            );

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
