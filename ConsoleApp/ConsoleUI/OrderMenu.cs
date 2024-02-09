using ConsoleApp.Entities;
using ConsoleApp.Services;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.ConsoleUI;

public class OrderMenu
{
    private readonly OrderService _orderService;

    public OrderMenu(OrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task ShowMenu()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Ordermeny:");
            Console.WriteLine("1. Skapa en ny order");
            Console.WriteLine("2. Hämta en order med ID");
            Console.WriteLine("3. Hämta alla ordrar");
            Console.WriteLine("4. Uppdatera en order");
            Console.WriteLine("5. Radera en order");
            Console.WriteLine("X. Återgå till huvudmenyn");
            Console.Write("\nVälj ett alternativ: ");

            var choice = Console.ReadLine();

            switch (choice!.ToUpper())
            {
                case "1":
                    await CreateOrder();
                    break;
                case "2":
                    await GetOrderById();
                    break;
                case "3":
                    await GetAllOrders();
                    break;
                case "4":
                    await UpdateOrder();
                    break;
                case "5":
                    await DeleteOrder();
                    break;
                case "X":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }
    }

    private async Task CreateOrder()
    {
        Console.WriteLine("Ange kundens ID för ordern:");
        var customerId = int.Parse(Console.ReadLine() ?? "0");

        // Antag att vi behöver samla in det totala priset för ordern.
        // Du kan utöka detta med en mer detaljerad process för att lägga till produkter och beräkna totalpriset.
        Console.WriteLine("Ange det totala priset för ordern:");
        var totalPrice = decimal.Parse(Console.ReadLine() ?? "0");

        // Vi använder det nuvarande datumet och tiden som orderdatum. Om annat datum krävs, samla in det från användaren.
        var orderDate = DateTime.Now;

        // Antag att CreateOrderAsync förväntar sig customerId, totalPrice, och orderDate som argument.
        var createdOrder = await _orderService.CreateOrderAsync(customerId, totalPrice, orderDate);
        Console.WriteLine($"Order skapad med ID: {createdOrder.Id}");

        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }



    private async Task GetOrderById()
    {
        Console.WriteLine("Ange orderns ID:");
        var orderId = int.Parse(Console.ReadLine() ?? "0");

        var order = await _orderService.GetOrderByIdAsync(orderId);
        if (order != null)
        {
            Console.WriteLine($"Order hittad: ID: {order.Id}, KundID: {order.CustomerId}, Orderdatum: {order.OrderDate}");
            // Lägg till mer information om ordern om så önskas
        }
        else
        {
            Console.WriteLine("Ordern hittades inte.");
        }

        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }


    private async Task GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        if (orders.Any())
        {
            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.Id}, KundID: {order.CustomerId}, Orderdatum: {order.OrderDate}");
                // Lägg till mer information om varje order om så önskas
            }
        }
        else
        {
            Console.WriteLine("Inga ordrar hittades.");
        }

        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }


    private async Task UpdateOrder()
    {
        Console.WriteLine("Ange ID för den order du vill uppdatera:");
        var orderId = int.Parse(Console.ReadLine() ?? "0");

        // Samla in den nya informationen som ska uppdateras
        Console.WriteLine("Ange det nya totalpriset för ordern:");
        var totalPrice = decimal.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("Ange det nya ordningsdatumet (åååå-mm-dd):");
        var orderDateString = Console.ReadLine();
        var orderDate = DateTime.Parse(orderDateString ?? DateTime.Now.ToString("yyyy-MM-dd"));

        // Anropa UpdateOrderAsync med de insamlade värdena
        var updatedOrder = await _orderService.UpdateOrderAsync(orderId, totalPrice, orderDate);
        Console.WriteLine($"Ordern med ID {updatedOrder.Id} har uppdaterats.");

        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }



    private async Task DeleteOrder()
    {
        Console.WriteLine("Ange ID för den order du vill radera:");
        var orderId = int.Parse(Console.ReadLine() ?? "0");

        var success = await _orderService.DeleteOrderAsync(orderId);
        if (success)
        {
            Console.WriteLine("Ordern har raderats.");
        }
        else
        {
            Console.WriteLine("Kunde inte radera ordern.");
        }

        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }

}
