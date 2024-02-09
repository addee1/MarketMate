using ConsoleApp.Services;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.ConsoleUI
{
    public class DetailMenu
    {
        private readonly DetailService _detailService;

        public DetailMenu(DetailService detailService)
        {
            _detailService = detailService;
        }

        public async Task ShowMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Välj ett alternativ:");
                Console.WriteLine("1. Skapa detalj");
                Console.WriteLine("2. Visa detalj");
                Console.WriteLine("3. Uppdatera detalj");
                Console.WriteLine("4. Visa alla detaljer");
                Console.WriteLine("5. Ta bort detalj");
                Console.WriteLine("X. Avsluta");
                Console.Write("\nVälj ett alternativ: ");

                var choice = Console.ReadLine();

                switch (choice!.ToUpper())
                {
                    case "1":
                        await CreateDetailAsync();
                        break;
                    case "2":
                        await ShowDetailAsync();
                        break;
                    case "3":
                        await UpdateDetailAsync();
                        break;
                    case "4":
                        await ShowAllDetailsAsync();
                        break;
                    case "5":
                        await DeleteDetailAsync();
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

        private async Task CreateDetailAsync()
        {
            Console.WriteLine("\nSkapa en ny detalj");

            Console.Write("Ange Order ID: ");
            int orderId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Ange Produkt ID: ");
            int productId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Ange Kvantitet: ");
            int quantity = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Ange Delsumma: ");
            decimal subtotal = decimal.Parse(Console.ReadLine() ?? "0.0");

            try
            {
                var detail = await _detailService.CreateDetailAsync(orderId, productId, quantity, subtotal);
                Console.WriteLine($"Detalj skapad med ID: {detail.Id}");

                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            }
        }


        private async Task ShowDetailAsync()
        {
            Console.WriteLine("\nVisa detalj");

            Console.Write("Ange Detalj ID: ");
            int detailId = int.Parse(Console.ReadLine() ?? "0");

            try
            {
                var detail = await _detailService.GetDetailByIdAsync(detailId);
                if (detail != null)
                {
                    Console.WriteLine($"ID: {detail.Id}, Order ID: {detail.OrderId}, Produkt ID: {detail.ProductId}, Kvantitet: {detail.Quantity}, Delsumma: {detail.Subtotal}");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Detalj hittades inte.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            }

            
        }


        private async Task UpdateDetailAsync()
        {
            Console.WriteLine("\nUppdatera detalj");

            Console.Write("Ange Detalj ID: ");
            int detailId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Ange ny kvantitet: ");
            int quantity = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Ange ny delsumma: ");
            decimal subtotal = decimal.Parse(Console.ReadLine() ?? "0.0");

            try
            {
                var updatedDetail = await _detailService.UpdateDetailAsync(detailId, quantity, subtotal);
                Console.WriteLine($"Detalj med ID {updatedDetail.Id} har uppdaterats.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            }
        }


        private async Task DeleteDetailAsync()
        {
            Console.WriteLine("\nTa bort detalj");

            Console.Write("Ange Detalj ID: ");
            int detailId = int.Parse(Console.ReadLine() ?? "0");

            try
            {
                bool success = await _detailService.DeleteDetailAsync(detailId);
                if (success)
                {
                    Console.WriteLine("Detalj borttagen.");
                }
                else
                {
                    Console.WriteLine("Detalj hittades inte eller kunde inte tas bort.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            }

            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }


        private async Task ShowAllDetailsAsync()
        {
            Console.WriteLine("\nVisa alla detaljer");

            try
            {
                var details = await _detailService.GetAllDetailsAsync();
                foreach (var detail in details)
                {
                    Console.WriteLine($"ID: {detail.Id}, Order ID: {detail.OrderId}, Produkt ID: {detail.ProductId}, Kvantitet: {detail.Quantity}, Delsumma: {detail.Subtotal}");
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            }

            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }

    }
}
