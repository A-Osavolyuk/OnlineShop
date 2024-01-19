using Microsoft.EntityFrameworkCore;
using OnlineShop.CouponApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.AddServiceDefaults();
builder.AddSqlServerDbContext<CouponDbContext>("Coupons", configureDbContextOptions: options =>
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.AddRedisOutputCache("Redis");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
