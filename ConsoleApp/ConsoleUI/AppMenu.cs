using ConsoleApp.Services;
namespace ConsoleApp.ConsoleUI;

public class AppMenu(CustomerService customerService, ProductService productService, CustomerProfileService customerProfileService, DetailService detailService, OrderService orderService, CustomerMenu customerMenu, CustomerProfileMenu customerProfileMenu, ProductMenu productMenu, OrderMenu orderMenu, DetailMenu detailMenu)
{
    private readonly CustomerService _customerService = customerService;
    private readonly ProductService _productService = productService;
    private readonly CustomerProfileService _customerProfileService = customerProfileService;
    private readonly DetailService _detailService = detailService;
    private readonly OrderService _orderService = orderService;
    private readonly CustomerMenu _customerMenu = customerMenu;
    private readonly CustomerProfileMenu _customerProfileMenu = customerProfileMenu;
    private readonly ProductMenu _productMenu = productMenu;
    private readonly OrderMenu _orderMenu = orderMenu;
    private readonly DetailMenu _detailMenu = detailMenu;


    public async Task Run()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Huvudmeny");
            Console.WriteLine("1. Kund Meny");
            Console.WriteLine("2. Kund-Profil Meny");
            Console.WriteLine("3. Produkt Meny");
            Console.WriteLine("4. Order Meny");
            Console.WriteLine("5. Detalj Meny");
            Console.WriteLine("X. Avsluta");
            
            Console.Write("\nVälj ett alternativ: ");
            var input = Console.ReadLine();

            switch (input?.ToUpper()) // Gör om till versaler
            {
                case "1":
                    await _customerMenu.ShowMenu();
                    break;
                case "2":
                    await _customerProfileMenu.ShowMenu();
                    break;
                case "3":
                    await _productMenu.ShowMenu();
                    break;
                case "4":
                    await _orderMenu.ShowMenu();
                    break;
                case "5":
                    await _detailMenu.ShowMenu();
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
                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }
    }



    










}
