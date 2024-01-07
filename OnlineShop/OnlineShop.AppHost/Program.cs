var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServerContainer("OnlineShop")
    .AddDatabase("Products");

builder.AddProject<Projects.OnlineShop_ProductApi>("ProductApi")
    .WithReference(sqlServer);

builder.Build().Run();
