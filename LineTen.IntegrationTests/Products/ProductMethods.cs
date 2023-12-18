using Application.LineTen.Products.Commands.CreateProduct;
using Application.LineTen.Products.Commands.UpdateProduct;
using Domain.LineTen.Products;
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

        public async Task<HttpResponseMessage?> CreateProduct(Product Product)
        {
            var command = new CreateProductCommand()
            {
                Name = Product.Name,
                Description = Product.Description,
                SKU = Product.SKU,
            };
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var postResponse = await _client.PostAsync("Products", content);
            return postResponse;
        }

        public async Task<HttpResponseMessage?> DeleteProduct(Guid ProductID)
        {
            var postResponse = await _client.DeleteAsync($"Products/{ProductID}");
            return postResponse;
        }

        public async Task<HttpResponseMessage?> GetProduct(Guid ProductID)
        {
            var postResponse = await _client.GetAsync($"Products/{ProductID}");
            return postResponse;
        }

        public async Task<HttpResponseMessage?> GetProducts()
        {
            var postResponse = await _client.GetAsync($"Products");
            return postResponse;
        }

        public async Task<HttpResponseMessage?> UpdateProduct(Product Product)
        {
            var command = new UpdateProductCommand()
            {
                ProductID = Product.ID.value,
                Name = Product.Name,
                Description = Product.Description,
                SKU = Product.SKU
            };
            var jsonContent = JsonSerializer.Serialize(command);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var postResponse = await _client.PutAsync("Products", content);
            return postResponse;
        }
    }
}
