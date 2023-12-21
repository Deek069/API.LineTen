using Application.LineTen.Products.DTOs;
using MediatR;

namespace Application.LineTen.Products.Commands.CreateProduct
{
    public sealed record CreateProductCommand(string Name, string Description, string SKU) : IRequest<ProductDTO>;
}
