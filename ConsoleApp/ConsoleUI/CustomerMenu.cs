using ConsoleApp.Entities;
using ConsoleApp.Repositories;
using ConsoleApp.Services;

namespace ConsoleApp.ConsoleUI;

public class CustomerMenu
{
    private readonly CustomerService _customerService;

    public CustomerMenu(CustomerService customerService)
    {
        _customerService = customerService;
    }


    public async Task ShowMenu()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Kund-Meny");
            Console.WriteLine("1. Lägg till en kund");
            Console.WriteLine("2. Hämta en kund med Id");
            Console.WriteLine("3. Hämta alla kunder");
            Console.WriteLine("4. Uppdatera en kund");
            Console.WriteLine("5. Radera en kund");
            Console.WriteLine("X. Återgå till huvudmenyn");

            Console.Write("\nVälj ett alternativ: ");
            var choice = Console.ReadLine();

            switch (choice!.ToUpper())
            {
                case "1":
                    await CreateCustomer();
                    break;
                case "2":
                    await GetCustomerById();
                    break;
                case "3":
                    await GetAllCustomers();
                    break;
                case "4":
                    await UpdateCustomer();
                    break;
                case "5":
                    await DeleteCustomer();
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


    public async Task CreateCustomer()
    {
        Console.WriteLine("Ange kundens e-post:");
        var email = Console.ReadLine();
        Console.WriteLine("Ange lösenord:");
        var password = Console.ReadLine();

        // Försöker skapa kunden och få tillbaka den nyskapade kunden
        var customer = await _customerService.CreateCustomerAsync(email!, password!);
        if (customer != null)
        {
            Console.WriteLine("Kund skapad med e-postadress: " + customer.Email);
        }
        else
        {
            Console.WriteLine("Kunden kunde inte skapas, e-postadressen kanske redan används.");
        }
    }

    public async Task GetCustomerById()
    {
        Console.WriteLine("Ange kundens ID:");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer != null)
            {
                Console.WriteLine($"Kund med Id:{customer.Id} hittad! E-postadress: {customer.Email}");
            }
            else
            {
                Console.WriteLine("Ingen kund hittades med det angivna ID:et.");
            }
        }
        else
        {
            Console.WriteLine("Ogiltigt ID-format.");
        }
    }


    public async Task GetAllCustomers()
    {
        Console.Clear();
        var customers = await _customerService.GetAllCustomersAsync();
        if (customers != null && customers.Any())
        {
            Console.WriteLine("Alla kunder:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.Id}, E-post: {customer.Email}");
            }
        }
        else
        {
            Console.WriteLine("Inga kunder hittades.");
        }
    }


    public async Task UpdateCustomer()
    {
        Console.WriteLine("Ange kundens ID som du vill uppdatera:");
        if (int.TryParse(Console.ReadLine(), out int customerIdToUpdate))
        {
            Console.WriteLine("Ange det nya lösenordet:");
            var newPassword = Console.ReadLine();

            try
            {
                var updatedCustomer = await _customerService.UpdateCustomerAsync(customerIdToUpdate, new CustomerEntity { Password = newPassword! });
                Console.WriteLine("Kunduppgifterna har uppdaterats för kund med ID: " + updatedCustomer.Id);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Ogiltigt kund-ID.");
        }
    }


    public async Task DeleteCustomer()
    {
        Console.WriteLine("Ange kundens ID som du vill radera:");
        if (int.TryParse(Console.ReadLine(), out int customerIdToDelete))
        {
            try
            {
                var isDeleted = await _customerService.DeleteCustomerAsync(customerIdToDelete);
                if (isDeleted)
                {
                    Console.WriteLine("Kunden har raderats.");
                }
                else
                {
                    Console.WriteLine("Kunden finns inte.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ett fel uppstod vid radering av kunden: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Ogiltigt kund-ID.");
        }
    }
}
