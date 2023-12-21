using Application.LineTen.Customers.Commands.CreateCustomer;
using Application.LineTen.Customers.DTOs;
using Domain.LineTen.Customers;

namespace LineTen.IntegrationTests.Customers
{
    internal sealed class CustomerTestData
    {
        public CreateCustomerCommand CreateCustomerCommand1 { get; set; }
        public CreateCustomerCommand CreateCustomerCommand2 { get; set; }

        public CustomerTestData()
        {
            CreateCustomerCommand1= new CreateCustomerCommand(
                "Joe",
                "Bloggs",
                "07723 2239356",
                "joe.bloggs@gamil.com"
            );

            CreateCustomerCommand2 = new CreateCustomerCommand(
                "Alice",
                "Smith",
                "09483 743738",
                "alice.smith@outlook.com"
            );
        }
    }
}
