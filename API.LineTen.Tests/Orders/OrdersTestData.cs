using Domain.LineTen.Orders;
using API.LineTen.Tests.Customers;
using API.LineTen.Tests.Products;

namespace API.LineTen.Tests.Orders
{
    internal sealed class OrdersTestData
    {
        public CustomerTestData CustomerTestData { get; set; }
        public ProductsTestData ProductTestData { get; set; }
        public Order Order1 { get; set; }
        public Order Order2 { get; set; }
        public Order Order3 { get; set; }
        public Order Order4 { get; set; }

        public OrdersTestData()
        {
            CustomerTestData = new CustomerTestData();
            ProductTestData = new ProductsTestData();

            Order1 = new Order()
            {
                ID = OrderID.CreateUnique(),

                CustomerID = CustomerTestData.Customer1.ID,
                Customer = CustomerTestData.Customer1,

                ProductID = ProductTestData.Product1.ID,
                Product = ProductTestData.Product1,

                Status = OrderStatus.Pending
            };

            Order2 = new Order()
            {
                ID = OrderID.CreateUnique(),

                CustomerID = CustomerTestData.Customer1.ID,
                Customer = CustomerTestData.Customer1,

                ProductID = ProductTestData.Product2.ID,
                Product = ProductTestData.Product2,

                Status = OrderStatus.Pending
            };

            Order3 = new Order()
            {
                ID = OrderID.CreateUnique(),

                CustomerID = CustomerTestData.Customer2.ID,
                Customer = CustomerTestData.Customer2,

                ProductID = ProductTestData.Product1.ID,
                Product = ProductTestData.Product1,

                Status = OrderStatus.Pending
            };

            Order4 = new Order()
            { 
                ID = OrderID.CreateUnique(),
                
                CustomerID = CustomerTestData.Customer2.ID,
                Customer = CustomerTestData.Customer2,
                
                ProductID = ProductTestData.Product2.ID,
                Product = ProductTestData.Product2,

                Status = OrderStatus.Pending
            };
        }
    }
}
