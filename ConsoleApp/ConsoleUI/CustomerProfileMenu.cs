using ConsoleApp.Entities;
using ConsoleApp.Services;
namespace ConsoleApp.ConsoleUI;

public class CustomerProfileMenu
{
    private readonly CustomerProfileService _customerProfileService;

    public CustomerProfileMenu(CustomerProfileService customerProfileService)
    {
        _customerProfileService = customerProfileService;
    }

    public async Task ShowMenu()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Kundprofil-Meny");
            Console.WriteLine("1. Skapa en Kund-Profil");
            Console.WriteLine("2. Hämta en profil med Id");
            Console.WriteLine("3. Hämta alla profiler");
            Console.WriteLine("4. Uppdatera en profil");
            Console.WriteLine("5. Radera Profil");
            Console.WriteLine("X. Återgå till huvudmenyn");
            Console.Write("\nVälj ett alternativ: ");

            string choice = Console.ReadLine()!;

            switch (choice!.ToUpper())
            {
                case "1":
                    await CreateProfile();
                    break;

                case "2":
                    await GetProfileById();
                    break;

                case "3":
                    await GetAllProfiles();
                    break;

                case "4":
                    await UpdateProfile();
                    break;

                case "5":
                    await DeleteProfile();
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

    public async Task CreateProfile()
    {
        Console.WriteLine("Ange kundens ID:");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Ange förnamn:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Ange efternamn:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Ange telefonnummer:");
            var phoneNumber = Console.ReadLine();

            try
            {
                var customerProfile = await _customerProfileService.CreateCustomerProfileAsync(customerId, firstName!, lastName!, phoneNumber!);
                Console.WriteLine("Kundprofil skapad med ID: " + customerProfile.CustomerId);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Kundeprofil kunde inte skapas: " + ex.Message);
            }

            Console.WriteLine("Tryck på en tangent för att fortsätta...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Ogiltigt kund-ID.");
        }
    }

    private async Task GetAllProfiles()
    {
        Console.WriteLine("Hämtar alla kundprofiler...");

        var customerProfiles = await _customerProfileService.GetAllCustomerProfilesAsync();

        if (customerProfiles.Any())
        {
            foreach (var profile in customerProfiles)
            {
                Console.WriteLine($"ID: {profile.CustomerId}, Förnamn: {profile.FirstName}, Efternamn: {profile.LastName}, Telefonnummer: {profile.PhoneNumber}");
            }
        }
        else
        {
            Console.WriteLine("Inga kundprofiler hittades.");
        }

        Console.WriteLine("Tryck på en tangent för att fortsätta...");
        Console.ReadKey();
    }


    private async Task GetProfileById()
    {
        Console.WriteLine("Ange CustomerProfile ID:");
        if (int.TryParse(Console.ReadLine(), out int customerProfileId))
        {
            var customerProfile = await _customerProfileService.GetCustomerProfileByIdAsync(customerProfileId);
            if (customerProfile != null)
            {
                Console.WriteLine("Kundprofil hittad:");
                Console.WriteLine($"ID: {customerProfile.CustomerId}");
                Console.WriteLine($"Förnamn: {customerProfile.FirstName}");
                Console.WriteLine($"Efternamn: {customerProfile.LastName}");
                Console.WriteLine($"Telefonnummer: {customerProfile.PhoneNumber}");
            }
            else
            {
                Console.WriteLine("Kundprofilen hittades inte.");
            }

            
            Console.WriteLine("Tryck på en tangent för att fortsätta...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Ogiltigt CustomerProfile ID.");
        }
    }


    private async Task UpdateProfile()
    {
        Console.WriteLine("Ange ID för den profil du vill uppdatera:");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Ange nytt förnamn:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Ange nytt efternamn:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Ange nytt telefonnummer:");
            var phoneNumber = Console.ReadLine();

            var updatedProfile = new CustomerProfileEntity
            {
                FirstName = string.IsNullOrWhiteSpace(firstName)? null : firstName,
                LastName = string.IsNullOrWhiteSpace(lastName)? null : lastName,
                PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber)? null : phoneNumber
            };

            try
            {
                var result = await _customerProfileService.UpdateCustomerProfileAsync(customerId, updatedProfile);
                Console.WriteLine($"Profilen för kund ID {result.CustomerId} har uppdaterats.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Ogiltigt ID.");
        }

        Console.WriteLine("Tryck på en tangent för att fortsätta...");
        Console.ReadKey();
    }


    private async Task DeleteProfile()
    {
        Console.WriteLine("Ange ID för den profil du vill radera:");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            try
            {
                var success = await _customerProfileService.DeleteCustomerProfileAsync(customerId);
                if (success)
                {
                    Console.WriteLine("Kundprofilen har raderats.");
                }
                else
                {
                    Console.WriteLine("Kunde inte hitta en profil med angivet ID att radera.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod vid försök att radera profilen: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ogiltigt ID.");
        }

        Console.WriteLine("Tryck på en tangent för att fortsätta...");
        Console.ReadKey();
    }


}
