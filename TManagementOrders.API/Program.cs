using TManagementOrders.Application.Service;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;
using TManagementOrders.Infrastructure.Data;
using TManagementOrders.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBaseInterface<Client>, BaseRepository<Client>>();
builder.Services.AddScoped<IBaseInterface<Product>, BaseRepository<Product>>();
builder.Services.AddScoped<IBaseInterface<Order>, BaseRepository<Order>>();


builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<OrderRepository>();

builder.Services.AddScoped<DapperContext>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Orders}/{action=Index}/{id?}");

app.Run();
