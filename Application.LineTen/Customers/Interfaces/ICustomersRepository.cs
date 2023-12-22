using Domain.LineTen.Entities;
using Domain.LineTen.ValueObjects.Customers;

namespace Application.LineTen.Customers.Interfaces
{
    public interface ICustomersRepository
    {
        void Create(Customer customer);
        Customer GetById(CustomerID customerID);
        IEnumerable<Customer> GetAll();
        void Update(Customer customer);
        void Delete(Customer customer);
        bool CustomerExists(CustomerID customerID);
    }
}
