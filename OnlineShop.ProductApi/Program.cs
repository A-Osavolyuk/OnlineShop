using FluentValidation;
using OnlineShop.ProductApi.Data;
using OnlineShop.ProductApi.Services;
using OnlineShop.ProductApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddSqlServerDbContext<ProductDbContext>("Products");
builder.AddRedisDistributedCache("Redis");
//builder.AddRedisOutputCache("Redis", configuration =>
//{
//    configuration.Tracing = true;
//    configuration.HealthChecks = true;
//    configuration.ConnectionString = "localhost:6379";
//},
//options =>
//{
//    options.ConnectRetry = 2;
//    options.ConnectTimeout = 900_000;
//});

builder.AddRedisOutputCache("Redis");

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        context.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
