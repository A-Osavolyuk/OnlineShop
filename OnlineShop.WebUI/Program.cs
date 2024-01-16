using MudBlazor.Services;
using OnlineShop.Infrastructure;
using OnlineShop.WebUI.Components;
using FluentValidation;
using OnlineShop.Infrastructure.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.ConfigureHttpClients();
builder.Services.ConfigureDependencyInjection();
builder.Services.AddOptionsConfiguration(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
