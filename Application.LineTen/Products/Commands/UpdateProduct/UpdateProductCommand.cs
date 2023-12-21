using MediatR;

namespace Application.LineTen.Products.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(Guid ID, string Name, string Description, string SKU) : IRequest<bool>;
}
