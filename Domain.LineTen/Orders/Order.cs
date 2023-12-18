using Domain.LineTen.Common.Interfaces;
using Domain.LineTen.Customers;
using Domain.LineTen.Products;

namespace Domain.LineTen.Orders
{
    public sealed class Order : IAuditableEntity
    {
        public OrderID ID { get; set; }

        public CustomerID CustomerID { get; set; }
        public Customer Customer { get; set; }

        public ProductID ProductID { get; set; }
        public Product Product { get; set; }

        public OrderStatus Status { get; set; }
    }
}
