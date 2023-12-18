using Application.LineTen.Customers.Commands.CreateCustomer;
using Application.LineTen.Customers.Commands.UpdateCustomer;
using Application.LineTen.Customers.DTOs;
using System.Net.Http.Json;
using System.Net;
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

        public async Task<CustomerDTO?> CreateCustomer(CreateCustomerCommand command)
        {
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("Customers", content);
            if (response.StatusCode != HttpStatusCode.Created) return null;
            var newCustomer = await response.Content.ReadFromJsonAsync<CustomerDTO>();
            return newCustomer;
        }

        public async Task<bool> DeleteCustomer(Guid customerID)
        {
            var response = await _client.DeleteAsync($"Customers/{customerID}");
            if (response.StatusCode != HttpStatusCode.OK) return false;
            return true;
        }

        public async Task<CustomerDTO?> GetCustomer(Guid customerID)
        {
            var response = await _client.GetAsync($"Customers/{customerID}");
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var customer = await response.Content.ReadFromJsonAsync<CustomerDTO>();
            return customer;
        }

        public async Task<List<CustomerDTO>?> GetCustomers()
        {
            var response = await _client.GetAsync($"Customers");
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var customers = await response.Content.ReadFromJsonAsync<List<CustomerDTO>>();
            return customers;
        }

        public async Task<bool> UpdateCustomer(UpdateCustomerCommand command)
        {
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("Customers", content);
            if (response.StatusCode != HttpStatusCode.OK) return false;
            return true;
        }
    }
}
