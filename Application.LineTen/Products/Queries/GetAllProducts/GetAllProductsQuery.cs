using Application.LineTen.Products.DTOs;
using MediatR;

namespace Application.LineTen.Products.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery(): IRequest<IEnumerable<ProductDTO>>;
}
