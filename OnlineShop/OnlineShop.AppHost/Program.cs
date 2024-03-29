var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServerContainer("OnlineShop")
    .AddDatabase("Products")
    .AddDatabase("Coupons");

var cache = builder.AddRedisContainer("Redis");

var productApi = builder.AddProject<Projects.OnlineShop_ProductApi>("ProductApi")
    .WithReference(cache)
    .WithReference(sqlServer);

builder.AddProject<Projects.OnlineShop_WebUI>("Blazor_WebUI")
    .WithReference(productApi);

builder.AddProject<Projects.OnlineShop_CouponApi>("CouponApi")
    .WithReference(cache)
    .WithReference(sqlServer);

builder.Build().Run();
