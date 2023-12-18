using Application.LineTen.Products.DTOs;
using MediatR;

namespace Application.LineTen.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommand : IRequest<ProductDTO>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
    }
}
