using Application.LineTen.Orders.Commands.CreateOrder;
using LineTen.IntegrationTests.Customers;
using LineTen.IntegrationTests.Products;

namespace LineTen.IntegrationTests.Orders
{
    internal sealed class OrdersTestData
    {
        public CustomerTestData CustomerTestData { get; set; }
        public ProductTestData ProductTestData { get; set; }

        public CreateOrderCommand CreateOrderCommand1 { get; set; }
        public CreateOrderCommand CreateOrderCommand2 { get; set; }
        public CreateOrderCommand CreateOrderCommand3 { get; set; }
        public CreateOrderCommand CreateOrderCommand4 { get; set; }

        public OrdersTestData()
        {
            CustomerTestData = new CustomerTestData();
            ProductTestData = new ProductTestData();
        }

        public async Task CreateTestDataAsync(HttpClient client)
        {
            var customerMethods = new CustomerMethods(client);
            var customer1 = await customerMethods.CreateCustomer(CustomerTestData.CreateCustomerCommand1);
            var customer2 = await customerMethods.CreateCustomer(CustomerTestData.CreateCustomerCommand2);

            var productMethods = new ProductMethods(client);
            var product1 = await productMethods.CreateProduct(ProductTestData.CreateProductCommand1);
            var product2 = await productMethods.CreateProduct(ProductTestData.CreateProductCommand2);

            CreateOrderCommand1 = new CreateOrderCommand(customer1.ID, product1.ID);
            CreateOrderCommand2 = new CreateOrderCommand(customer1.ID, product2.ID);
            CreateOrderCommand3 = new CreateOrderCommand(customer2.ID, product1.ID);
            CreateOrderCommand4 = new CreateOrderCommand(customer2.ID, product2.ID);
        }
    }
}
