using Domain.LineTen.Common.Interfaces;
using Domain.LineTen.ValueObjects.Customers;
using Domain.LineTen.ValueObjects.Orders;
using Domain.LineTen.ValueObjects.Products;

namespace Domain.LineTen.Entities
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
