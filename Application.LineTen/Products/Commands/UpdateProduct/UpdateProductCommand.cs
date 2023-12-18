using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommand : IRequest<bool>
    {
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
    }
}
