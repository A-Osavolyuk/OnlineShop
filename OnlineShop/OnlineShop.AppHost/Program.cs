var builder = DistributedApplication.CreateBuilder(args);



builder.AddProject<Projects.OnlineShop_ProductApi>("onlineshop.productapi");



builder.Build().Run();
