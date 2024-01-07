var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServerContainer("OnlineShop")
    .AddDatabase("Products");

var cache = builder.AddRedisContainer("Redis");

builder.AddProject<Projects.OnlineShop_ProductApi>("ProductApi")
    .WithReference(sqlServer)
    .WithReference(cache);

builder.Build().Run();
