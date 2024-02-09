using ConsoleApp.Entities;
using ConsoleApp.Services;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.ConsoleUI;

public class ProductMenu
{
    private readonly ProductService _productService;

    public ProductMenu(ProductService productService)
    {
        _productService = productService;
    }


    public async Task ShowMenu()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Produkt-Meny");
            Console.WriteLine("1. Skapa en ny produkt");
            Console.WriteLine("2. Hämta en produkt med ID");
            Console.WriteLine("3. Hämta alla produkter");
            Console.WriteLine("4. Uppdatera en produkt");
            Console.WriteLine("5. Radera en produkt");
            Console.WriteLine("X. Återgå till huvudmenyn");
            Console.Write("\nVälj ett alternativ: ");

            var choice = Console.ReadLine();

            switch (choice!.ToUpper())
            {
                case "1":
                    await CreateProduct();
                    break;
                case "2":
                    await GetProductById();
                    break;
                case "3":
                    await GetAllProducts();
                    break;
                case "4":
                    await UpdateProduct();
                    break;
                case "5":
                    await DeleteProduct();
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


    private async Task CreateProduct()
    {
        Console.WriteLine("Ange produktnamn:");
        var productName = Console.ReadLine();
        Console.WriteLine("Ange beskrivning:");
        var description = Console.ReadLine();
        Console.WriteLine("Ange pris:");
        var price = decimal.Parse(Console.ReadLine() ?? "0");
        Console.WriteLine("Ange lagerstatus:");
        var stockStatus = int.Parse(Console.ReadLine() ?? "0");

        var product = new ProductEntity
        {
            ProductName = productName!,
            Description = description,
            Price = price,
            StockStatus = stockStatus
        };

        var createdProduct = await _productService.CreateProductAsync(productName!, description!, price, stockStatus);
        Console.WriteLine($"Produkt skapad: {createdProduct.ProductName}");

        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }


    private async Task GetProductById()
    {
        Console.WriteLine("Ange produktens ID:");
        var productId = int.Parse(Console.ReadLine() ?? "0");

        var product = await _productService.GetProductByIdAsync(productId);
        if (product != null)
        {
            Console.WriteLine($"Produkt hittad: {product.ProductName}, Pris: {product.Price}");
        }
        else
        {
            Console.WriteLine("Produkten hittades inte.");
        }

        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }


    private async Task GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        if (products.Any())
        {
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Namn: {product.ProductName}, Pris: {product.Price}");
            }
        }
        else
        {
            Console.WriteLine("Inga produkter hittades.");
        }

        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }


    private async Task UpdateProduct()
    {
        Console.WriteLine("Ange ID för den produkt du vill uppdatera:");
        var productIdInput = Console.ReadLine();
        if (!int.TryParse(productIdInput, out var productId))
        {
            Console.WriteLine("Ogiltigt ID format.");
            return;
        }

        Console.WriteLine("Ange nytt produktnamn (lämna tomt om oförändrat):");
        var productName = Console.ReadLine();

        Console.WriteLine("Ange ny beskrivning (lämna tomt om oförändrat):");
        var description = Console.ReadLine();

        Console.WriteLine("Ange nytt pris (lämna tomt om oförändrat):");
        var priceInput = Console.ReadLine();
        if (!decimal.TryParse(priceInput, out var price))
        {
            Console.WriteLine("Ogiltigt pris format.");
            return;
        }

        Console.WriteLine("Ange ny lagerstatus (lämna tomt om oförändrat):");
        var stockStatusInput = Console.ReadLine();
        if (!int.TryParse(stockStatusInput, out var stockStatus))
        {
            Console.WriteLine("Ogiltigt format för lagerstatus.");
            return;
        }

        try
        {
            var updatedProduct = await _productService.UpdateProductAsync(productId, productName!, description!, price, stockStatus);
            Console.WriteLine($"Produkten med ID {updatedProduct.Id} har uppdaterats.");
        }
        catch (KeyNotFoundException knf)
        {
            Console.WriteLine(knf.Message);
        }

        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }


    private async Task DeleteProduct()
    {
        Console.WriteLine("Ange ID för den produkt du vill radera:");
        var productId = int.Parse(Console.ReadLine() ?? "0");

        var success = await _productService.DeleteProductAsync(productId);
        if (success)
        {
            Console.WriteLine("Produkten har raderats.");
        }
        else
        {
            Console.WriteLine("Kunde inte radera produkten.");
        }

        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }

}