using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence.LineTen;

namespace LineTen.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        private const string _apiKeyName = "x-api-key";
        private const string _apiKeyValue = "ec6254fd-ea6b-4f67-afc6-406c946ed854";

        public IntegrationTest()
        {
            var appFactory = new APIFactory();
            TestClient = appFactory.CreateClient();
            TestClient.DefaultRequestHeaders.Add(_apiKeyName, _apiKeyValue);
        }
    }
}
