using Domain.LineTen.Customers;
using Microsoft.Identity.Client;
using Persistence.LineTen.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace API.LineTen.Tests.Customers
{
    internal sealed class CustomerTestData
    {
        public Customer Customer1 { get; set; }
        public Customer Customer2 { get; set; }

        public CustomerTestData()
        {
            Customer1 = new Customer()
            {
                ID = CustomerID.CreateUnique(),
                FirstName = "Joe",
                LastName = "Bloggs",
                Phone = "07723 2239356",
                Email = "joe.bloggs@gamil.com"
            };

            Customer2 = new Customer()
            {
                ID = CustomerID.CreateUnique(),
                FirstName = "Alice",
                LastName = "Smith",
                Phone = "09483 743738",
                Email = "alice.smith@outlook.com"
            };
        }
    }
}
