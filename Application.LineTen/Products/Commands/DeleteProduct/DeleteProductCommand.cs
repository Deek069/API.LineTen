using MediatR;

namespace Application.LineTen.Products.Commands.DeleteProduct
{
    public sealed record DeleteProductCommand(Guid ID) : IRequest;
}
