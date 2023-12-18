using Application.LineTen.Customers.Interfaces;
using Domain.LineTen.Customers;

namespace Persistence.LineTen.Repositories
{
    public sealed class CustomersRepository : ICustomersRepository
    {
        private LineTenDB db;

        public CustomersRepository(LineTenDB dbContent) { 
           db = dbContent;
        }

        public void Create(Customer customer)
        {
            db.Customers.Add(customer);
        }

        public bool CustomerExists(CustomerID customerID)
        {
            var result = db.Customers.Any(c => c.ID == customerID);
            return result;
        }

        public void Delete(Customer customer)
        {
            db.Customers.Remove(customer);
        }

        public IEnumerable<Customer> GetAll()
        {
            var result = db.Customers;
            return result;
        }

        public Customer GetById(CustomerID customerID)
        {
            var result = db.Customers.Find(customerID);
            return result;
        }

        public void Update(Customer customer)
        {
            db.Customers.Update(customer);
        }
    }
}
