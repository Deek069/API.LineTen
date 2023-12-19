using Persistence.LineTen;
using Persistence.LineTen.Repositories;
using Application.LineTen.Common.Interfaces;
using Application.LineTen.Customers.Interfaces;
using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using API.LineTen.Authentication;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("dbConnection");
builder.Services.AddDbContext<LineTenDB>(options =>
    options.UseSqlServer(connection)
);
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Application.LineTen.Customers.Queries.GetAllCustomers.GetAllCustomersQuery>());


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter the API key",
        Name = "x-api-key",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "ApiKeyScheme"
    });

    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };

    var requirement = new OpenApiSecurityRequirement
    {
        {key, new List<string>() }
    };

    options.AddSecurityRequirement(requirement);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
