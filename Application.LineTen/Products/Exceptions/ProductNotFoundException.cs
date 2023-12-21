using Domain.LineTen.Products;

namespace Application.LineTen.Products.Exceptions
{
    public sealed class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(ProductID id)
            : base($"The product with ID {id.value} was not found.")
        {
        }
    }
}
