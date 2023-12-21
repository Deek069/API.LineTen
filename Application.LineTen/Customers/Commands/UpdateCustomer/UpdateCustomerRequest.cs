namespace Application.LineTen.Customers.Commands.UpdateCustomer
{
    public sealed record UpdateCustomerRequest(string FirstName, string LastName, string Phone, string Email);
}
