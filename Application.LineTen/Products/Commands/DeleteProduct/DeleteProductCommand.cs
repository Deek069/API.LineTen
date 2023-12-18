using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommand : IRequest<bool>
    {
        public ProductID ProductID { get; set; }
    }
}
