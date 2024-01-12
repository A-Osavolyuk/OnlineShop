using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductApi.Data;
using OnlineShop.ProductApi.Services;
using OnlineShop.ProductApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddSqlServerDbContext<ProductDbContext>("Products", configureDbContextOptions: options =>
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.AddRedisOutputCache("Redis");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
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
