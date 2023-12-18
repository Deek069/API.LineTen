using Application.LineTen.Orders.DTOs;
using Domain.LineTen.Orders;
using System.Security.Cryptography.X509Certificates;

namespace Application.LineTen.Tests.Orders
{
    internal sealed class OrdersTestData
    {
        public Customers.CustomerTestData CustomerTestData { get; set; }
        public Products.ProductsTestData ProductTestData { get; set; }
        public Order Order1 { get; set; }
        public Order Order2 { get; set; }
        public Order Order3 { get; set; }
        public Order Order4 { get; set; }

        public OrdersTestData()
        {
            CustomerTestData = new Customers.CustomerTestData();
            ProductTestData = new Products.ProductsTestData();

            Order1 = new Order()
            {
                ID = OrderID.CreateUnique(),

                CustomerID = CustomerTestData.Customer1.ID,
                Customer = CustomerTestData.Customer1,

                ProductID = ProductTestData.Product1.ID,
                Product = ProductTestData.Product1,

                Status = OrderStatus.Pending,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            Order2 = new Order()
            {
                ID = OrderID.CreateUnique(),

                CustomerID = CustomerTestData.Customer1.ID,
                Customer = CustomerTestData.Customer1,

                ProductID = ProductTestData.Product2.ID,
                Product = ProductTestData.Product2,

                Status = OrderStatus.Pending,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            Order3 = new Order()
            {
                ID = OrderID.CreateUnique(),

                CustomerID = CustomerTestData.Customer2.ID,
                Customer = CustomerTestData.Customer2,

                ProductID = ProductTestData.Product1.ID,
                Product = ProductTestData.Product1,

                Status = OrderStatus.Pending,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            Order4 = new Order()
            { 
                ID = OrderID.CreateUnique(),

                CustomerID = CustomerTestData.Customer2.ID,
                Customer = CustomerTestData.Customer2,

                ProductID = ProductTestData.Product2.ID,
                Product = ProductTestData.Product2,

                Status = OrderStatus.Pending,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
        }
    }
}
