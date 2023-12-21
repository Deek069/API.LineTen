using Application.LineTen.Customers.Exceptions;
using Application.LineTen.Orders.Commands.CreateOrder;
using Application.LineTen.Orders.Commands.DeleteOrder;
using Application.LineTen.Orders.Commands.UpdateOrder;
using Application.LineTen.Orders.DTOs;
using Application.LineTen.Orders.Exceptions;
using Application.LineTen.Orders.Queries.GetAllOrders;
using Application.LineTen.Orders.Queries.GetOrderSummary;
using Application.LineTen.Products.Exceptions;
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
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(CreateOrder), new { id = result.ID }, result);
            }
            catch (ProductNotFoundException px)
            {
                return NotFound(px.Message);
            } catch (CustomerNotFoundException cs)
            {
                return NotFound(cs.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OrderSummaryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllOrdersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderSummaryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetOrderSummary(Guid id)
        {
            try
            {
                var query = new GetOrderSummaryQuery(id);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (OrderNotFoundException ox)
            {
                return NotFound(ox.Message);
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
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderRequest request)
        {
            try
            {
                var command = new UpdateOrderCommand(id, request.Status);
                await _mediator.Send(command);
                return Ok();
            }
            catch (OrderNotFoundException px)
            {
                return NotFound(px.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                var command = new DeleteOrderCommand(id);
                await _mediator.Send(command);
                return Ok();
            }
            catch (OrderNotFoundException px)
            {
                return NotFound(px.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
