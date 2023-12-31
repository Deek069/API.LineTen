﻿using Domain.LineTen.ValueObjects.Customers;

namespace Domain.LineTen.Entities
{
    public sealed class Customer
    {
        public CustomerID ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public List<Order> Orders { get; set; }
    }
}
