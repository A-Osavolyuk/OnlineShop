var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServerContainer("OnlineShop")
    .AddDatabase("Products");

var cache = builder.AddRedisContainer("Redis");

builder.AddProject<Projects.OnlineShop_ProductApi>("ProductApi")
    .WithReference(cache)
    .WithReference(sqlServer);

builder.Build().Run();
