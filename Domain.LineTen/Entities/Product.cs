using Domain.LineTen.ValueObjects.Products;

namespace Domain.LineTen.Entities
{
    public sealed class Product
    {
        public ProductID ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }

        public List<Order> Orders { get; set; }
    }
}
