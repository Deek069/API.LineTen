using Application.LineTen.Products.DTOs;
using MediatR;

namespace Application.LineTen.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery: IRequest<IEnumerable<ProductDTO>>
    {
    }
}
