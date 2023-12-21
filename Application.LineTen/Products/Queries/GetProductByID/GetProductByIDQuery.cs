using Application.LineTen.Products.DTOs;
using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Products.Queries.GetProductByID
{
    public sealed record GetProductByIDQuery(ProductID ID) : IRequest<ProductDTO>;
}
