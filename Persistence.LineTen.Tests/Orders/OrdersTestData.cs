using Persistence.LineTen.Tests.Customers;
using Persistence.LineTen.Tests.Products;
using Application.LineTen.Common.Interfaces;
using Persistence.LineTen.Repositories;
using Domain.LineTen.Entities;
using Domain.LineTen.ValueObjects.Orders;

namespace Persistence.LineTen.Tests.Orders
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
                ProductID = ProductTestData.Product1.ID,
                Status = OrderStatus.Pending
            };

            Order2 = new Order()
            {
                ID = OrderID.CreateUnique(),
                CustomerID = CustomerTestData.Customer1.ID,
                ProductID = ProductTestData.Product2.ID,
                Status = OrderStatus.Pending
            };

            Order3 = new Order()
            {
                ID = OrderID.CreateUnique(),
                CustomerID = CustomerTestData.Customer2.ID,
                ProductID = ProductTestData.Product1.ID,
                Status = OrderStatus.Pending
            };

            Order4 = new Order()
            { 
                ID = OrderID.CreateUnique(),
                CustomerID = CustomerTestData.Customer2.ID,
                ProductID = ProductTestData.Product2.ID,
                Status = OrderStatus.Pending
            };
        }

        public async Task CreateTestCustomersAndProducts(LineTenDB db, IUnitOfWork unitOfWork)
        {
            var customerRepo = new CustomersRepository(db);
            customerRepo.Create(CustomerTestData.Customer1);
            customerRepo.Create(CustomerTestData.Customer2);

            var productRepo = new ProductsRepository(db);
            productRepo.Create(ProductTestData.Product1);
            productRepo.Create(ProductTestData.Product2);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
