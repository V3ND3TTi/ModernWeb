using Northwind.EntityModels; // To use AddNorthwindContext method.
using Microsoft.Extensions.Caching.Hybrid; // To use HybridCacheEntryOptions.
using Northwind.WebApi.Repositories; // To use ICustomerRepository.
//using Microsoft.AspNetCore.HttpLogging;

const string corsPolicyName = "allowWasmClient";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(documentName: "v2");

builder.Services.AddNorthwindContext();

builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromSeconds(60),
        LocalCacheExpiration = TimeSpan.FromSeconds(30)
    };
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

/*builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096; // default is 32k
    options.ResponseBodyLogLimit = 4096; // default is 32k
});*/

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
        {
            policy.WithOrigins("https://localhost:5152", "http://localhost:5153");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.MapGet("/weatherforecast/{days:int?}", (int days = 5) => GetWeather(days)).WithName("GetWeatherForecast");

app.MapCustomers();

app.Run();