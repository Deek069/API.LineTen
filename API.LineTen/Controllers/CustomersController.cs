using Application.LineTen.Customers.Commands.CreateCustomer;
using Application.LineTen.Customers.Commands.DeleteCustomer;
using Application.LineTen.Customers.Commands.UpdateCustomer;
using Application.LineTen.Customers.DTOs;
using Application.LineTen.Customers.Exceptions;
using Application.LineTen.Customers.Queries.GetAllCustomers;
using Application.LineTen.Customers.Queries.GetCustomerByID;
using Domain.LineTen.ValueObjects.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.LineTen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IMediator _mediator;

        public CustomersController(ILogger<CustomersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(CreateCustomer), new { id = result.ID }, result);
            }
            catch (CustomerValidationException cx)
            {
                return BadRequest(cx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCustomersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var query = new GetCustomerByIDQuery(new CustomerID(id));
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (CustomerNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerRequest request)
        {
            var command = new UpdateCustomerCommand(id, request.FirstName, request.LastName, request.Phone, request.Email);
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (CustomerNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var command = new DeleteCustomerCommand(id);
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (CustomerNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
