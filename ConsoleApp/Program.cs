using ConsoleApp.ConsoleUI;
using ConsoleApp.Context;
using ConsoleApp.Repositories;
using ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\code\Datalagring\MarketMateApp\ConsoleApp\ConsoleApp\Data\local_database.mdf;Integrated Security=True;Connect Timeout=30"));

    services.AddScoped<CustomerRepository>();
    services.AddScoped<CustomerProfileRepository>();
    services.AddScoped<OrderRepository>();
    services.AddScoped<DetailRepository>();
    services.AddScoped<ProductRepository>();

    services.AddScoped<CustomerService>();
    services.AddScoped<CustomerProfileService>();
    services.AddScoped<OrderService>();
    services.AddScoped<DetailService>();
    services.AddScoped<ProductService>();

    services.AddScoped<AppMenu>();
    services.AddScoped<CustomerMenu>();
    services.AddScoped<CustomerProfileMenu>();
    services.AddScoped<ProductMenu>();
    services.AddScoped<OrderMenu>();
    services.AddScoped<DetailMenu>();



}).Build();



// Startar appmenyn
var host = builder;
var appMenu = host.Services.GetRequiredService<AppMenu>();
await appMenu.Run();






