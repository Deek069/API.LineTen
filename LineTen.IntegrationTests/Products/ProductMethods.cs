using Application.LineTen.Products.Commands.CreateProduct;
using Application.LineTen.Products.Commands.UpdateProduct;
using Application.LineTen.Products.DTOs;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace LineTen.IntegrationTests.Products
{
    internal class ProductMethods
    {
        private HttpClient _client;

        public ProductMethods(HttpClient Client)
        {
            _client = Client;
        }

        public async Task<ProductDTO?> CreateProduct(CreateProductCommand command)
        {
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("Products", content);
            if (response.StatusCode != HttpStatusCode.Created) return null;
            var newProduct = await response.Content.ReadFromJsonAsync<ProductDTO>();
            return newProduct;
        }

        public async Task<bool> DeleteProduct(Guid ProductID)
        {
            var response = await _client.DeleteAsync($"Products/{ProductID}");
            if (response.StatusCode != HttpStatusCode.OK) return false;
            return true;
        }

        public async Task<ProductDTO?> GetProduct(Guid ProductID)
        {
            var response = await _client.GetAsync($"Products/{ProductID}");
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var Product = await response.Content.ReadFromJsonAsync<ProductDTO>();
            return Product;
        }

        public async Task<List<ProductDTO>?> GetProducts()
        {
            var response = await _client.GetAsync($"Products");
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var Products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
            return Products;
        }

        public async Task<bool> UpdateProduct(UpdateProductCommand command)
        {
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("Products", content);
            if (response.StatusCode != HttpStatusCode.OK) return false;
            return true;
        }
    }
}
