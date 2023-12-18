using API.LineTen.Controllers;
using Application.LineTen.Customers.DTOs;
using Application.LineTen.Customers.Queries.GetAllCustomers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;

namespace API.LineTen.Tests.Customers
{
    public class GetCustomers_Tests
    {
        private Mock<ILogger<CustomersController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private CustomersController _customersController;
        private CustomerTestData _customerTestData;

        public GetCustomers_Tests()
        {
            _mockLogger = new Mock<ILogger<CustomersController>>();
            _mockMediator = new Mock<IMediator>();
            _customersController = new CustomersController(_mockLogger.Object, _mockMediator.Object);
            _customerTestData = new CustomerTestData();
        }

        [Fact]
        public async Task GetCustomers_Should_ReturnAllCustomers()
        {
            // Arrange
            var expectedData = new List<CustomerDTO> { CustomerDTO.FromCustomer(_customerTestData.Customer1),
                                                       CustomerDTO.FromCustomer(_customerTestData.Customer2) };
            _mockMediator.Setup(x => x.Send(It.IsAny<GetAllCustomersQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedData);

            // Act
            var result = (ActionResult)await _customersController.GetAll();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var customers = (List<CustomerDTO>)actionResult.Value;
            Assert.Equal(expected: expectedData.Count, actual: customers.Count);
            Assert.Equal(expected: expectedData[0], actual: customers[0]);
            Assert.Equal(expected: expectedData[1], actual: customers[1]);
        }
    }
}
