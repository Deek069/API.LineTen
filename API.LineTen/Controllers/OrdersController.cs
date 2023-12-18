using Application.LineTen.Orders.Commands.CreateOrder;
using Application.LineTen.Orders.Commands.DeleteOrder;
using Application.LineTen.Orders.Commands.UpdateOrder;
using Application.LineTen.Orders.Queries.GetAllOrders;
using Application.LineTen.Orders.Queries.GetOrderByID;
using Domain.LineTen.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.LineTen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;

        public OrdersController(ILogger<OrdersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateOrder), new { id = result.ID }, result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllOrdersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var query = new GetOrderByIDQuery() { OrderID = id };
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
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand() { OrderID = id };
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
