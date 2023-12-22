using Application.LineTen.Products.Commands.CreateProduct;
using Application.LineTen.Products.Commands.DeleteProduct;
using Application.LineTen.Products.Commands.UpdateProduct;
using Application.LineTen.Products.DTOs;
using Application.LineTen.Products.Exceptions;
using Application.LineTen.Products.Queries.GetAllProducts;
using Application.LineTen.Products.Queries.GetProductByID;
using Domain.LineTen.ValueObjects.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.LineTen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;

        public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(CreateProduct), new { id = result.ID }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByID(Guid id)
        {
            try
            {
                var query = new GetProductByIDQuery(new ProductID(id));
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (ProductNotFoundException ox)
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
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request)
        {
            try
            {
                var command = new UpdateProductCommand(id, request.Name, request.Description, request.SKU);
                await _mediator.Send(command);
                return Ok();
            }
            catch (ProductNotFoundException px)
            {
                return NotFound(px.Message);
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
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var command = new DeleteProductCommand(id);
                await _mediator.Send(command);
                return Ok();
            }
            catch (ProductNotFoundException px)
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
