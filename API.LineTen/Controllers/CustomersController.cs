﻿using Application.LineTen.Customers.Commands.CreateCustomer;
using Application.LineTen.Customers.Commands.DeleteCustomer;
using Application.LineTen.Customers.Commands.UpdateCustomer;
using Application.LineTen.Customers.Queries.GetAllCustomers;
using Application.LineTen.Customers.Queries.GetCustomerByID;
using Domain.LineTen.Customers;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer(string firstName, string lastName, string phone, string email)
        {
            var command = new CreateCustomerCommand()
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Email = email
            };
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateCustomer), new { id = result.ID }, result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCustomersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var query = new GetCustomerByIDQuery() { ID = new CustomerID(id) };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(Guid customerID, string firstName, string lastName, string phone, string email)
        {
            var command = new UpdateCustomerCommand()
            {
                ID = new CustomerID(customerID),
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Email = email
            };
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var command = new DeleteCustomerCommand() { ID = new CustomerID(id) };
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return Ok();
        }
    }
}