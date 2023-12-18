using Application.LineTen.Customers.Commands.CreateCustomer;
using Application.LineTen.Customers.Commands.UpdateCustomer;
using Domain.LineTen.Customers;
using System.Text;
using System.Text.Json;

namespace LineTen.IntegrationTests.Customers
{
    internal class CustomerMethods
    {
        private HttpClient _client;

        public CustomerMethods(HttpClient Client) { 
            _client = Client;
        }

        public async Task<HttpResponseMessage?> CreateCustomer(Customer customer)
        {
            var command = new CreateCustomerCommand()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone,
                Email = customer.Email
            };
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var postResponse = await _client.PostAsync("Customers", content);
            return postResponse;
        }

        public async Task<HttpResponseMessage?> DeleteCustomer(Guid customerID)
        {
            var postResponse = await _client.DeleteAsync($"Customers/{customerID}");
            return postResponse;
        }

        public async Task<HttpResponseMessage?> GetCustomer(Guid customerID)
        {
            var postResponse = await _client.GetAsync($"Customers/{customerID}");
            return postResponse;
        }

        public async Task<HttpResponseMessage?> GetCustomers()
        {
            var postResponse = await _client.GetAsync($"Customers");
            return postResponse;
        }

        public async Task<HttpResponseMessage?> UpdateCustomer(Customer customer)
        {
            var command = new UpdateCustomerCommand()
            {
                CustomerID = customer.ID.value,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone, 
                Email = customer.Email
            };
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var postResponse = await _client.PutAsync("Customers", content);
            return postResponse;
        }
    }
}
