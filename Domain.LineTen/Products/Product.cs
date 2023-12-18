using Domain.LineTen.Orders;

namespace Domain.LineTen.Products
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
