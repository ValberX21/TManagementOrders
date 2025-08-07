using TManagementOrders.Application.Service;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;
using TManagementOrders.Domain.Interfaces.Repository;
using TManagementOrders.Infrastructure.Data;
using TManagementOrders.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DapperContext>();

builder.Services.AddScoped(typeof(IBaseInterfaceRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ClientRepository>(); 
builder.Services.AddScoped<OrderRepository>();

builder.Services.AddScoped<IBaseInterfaceService<Client>, ClientService>();
builder.Services.AddScoped<IBaseInterfaceService<Product>, ProductService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();

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
