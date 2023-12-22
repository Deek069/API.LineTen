using API.LineTen.Controllers;
using Application.LineTen.Customers.DTOs;
using Application.LineTen.Customers.Queries.GetCustomerByID;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using Application.LineTen.Customers.Exceptions;
using Domain.LineTen.ValueObjects.Customers;

namespace API.LineTen.Tests.Customers.Tests
{
    public class GetCustomer_Tests
    {
        private Mock<ILogger<CustomersController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private CustomersController _customersController;
        private CustomerTestData _customerTestData;

        public GetCustomer_Tests()
        {
            _mockLogger = new Mock<ILogger<CustomersController>>();
            _mockMediator = new Mock<IMediator>();
            _customersController = new CustomersController(_mockLogger.Object, _mockMediator.Object);
            _customerTestData = new CustomerTestData();
        }

        [Fact]
        public async Task GetCustomer_Should_ReturnCustomer_WithValidID()
        {
            // Arrange
            var expectedCustomer = CustomerDTO.FromCustomer(_customerTestData.Customer1);
            _mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerByIDQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedCustomer);

            // Act
            var result = (ActionResult)await _customersController.GetByID(_customerTestData.Customer1.ID.value);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var customer = (CustomerDTO)actionResult.Value;
            Assert.Equal(expected: expectedCustomer, actual: customer);
        }

        [Fact]
        public async Task GetCustomer_Should_ReturnNotFound_WithInvalidID()
        {
            // Arrange
            var customerID = CustomerID.CreateUnique();
            _mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerByIDQuery>(), It.IsAny<CancellationToken>()))
                        .Throws(new CustomerNotFoundException(customerID));

            // Act
            var result = (ActionResult)await _customersController.GetByID(customerID.value);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
