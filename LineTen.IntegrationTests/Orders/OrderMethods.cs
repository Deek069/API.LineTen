using Application.LineTen.Orders.Commands.CreateOrder;
using Application.LineTen.Orders.Commands.UpdateOrder;
using Application.LineTen.Orders.DTOs;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace LineTen.IntegrationTests.Orders
{
    internal class OrderMethods
    {
        private HttpClient _client;

        public OrderMethods(HttpClient Client)
        {
            _client = Client;
        }

        public async Task<OrderDTO?> CreateOrder(CreateOrderCommand command)
        {
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("Orders", content);
            if (response.StatusCode != HttpStatusCode.Created) return null;
            var newOrder = await response.Content.ReadFromJsonAsync<OrderDTO>();
            return newOrder;
        }

        public async Task<bool> DeleteOrder(Guid OrderID)
        {
            var response = await _client.DeleteAsync($"Orders/{OrderID}");
            if (response.StatusCode != HttpStatusCode.OK) return false;
            return true;
        }

        public async Task<OrderDTO?> GetOrder(Guid OrderID)
        {
            var response = await _client.GetAsync($"Orders/{OrderID}");
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var Order = await response.Content.ReadFromJsonAsync<OrderDTO>();
            return Order;
        }

        public async Task<List<OrderDTO>?> GetOrders()
        {
            var response = await _client.GetAsync($"Orders");
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var Orders = await response.Content.ReadFromJsonAsync<List<OrderDTO>>();
            return Orders;
        }

        public async Task<bool> UpdateOrder(UpdateOrderCommand command)
        {
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("Orders", content);
            if (response.StatusCode != HttpStatusCode.OK) return false;
            return true;
        }
    }
}
