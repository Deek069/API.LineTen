using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;

namespace LineTen.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        private const string _apiKeyName = "x-api-key";
        private const string _apiKeyValue = "ec6254fd-ea6b-4f67-afc6-406c946ed854";

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DbContext));
                        services.AddDbContext<DbContext>( 
                            optionsAction: options => { options.UseInMemoryDatabase(databaseName: "TestDatabase"); 
                        });
                    });
                });
            TestClient = appFactory.CreateClient();
            TestClient.DefaultRequestHeaders.Add(_apiKeyName, _apiKeyValue);
        }
    }
}
