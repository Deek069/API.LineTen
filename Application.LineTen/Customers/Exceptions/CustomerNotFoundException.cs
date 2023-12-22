using Domain.LineTen.ValueObjects.Customers;

namespace Application.LineTen.Customers.Exceptions
{
    public sealed class CustomerNotFoundException: Exception
    {
        public CustomerNotFoundException(CustomerID id) 
            : base($"The customer with ID {id.value} was not found.")
        {
        }
    }
}
