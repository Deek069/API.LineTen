using Domain.LineTen.ValueObjects.Customers;

namespace Application.LineTen.Customers.Exceptions
{
    public sealed class CustomerValidationException : Exception
    {
        public CustomerValidationException(string message) 
            : base(message)
        {
        }
    }
}
