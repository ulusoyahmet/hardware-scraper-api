using System;
using System.Linq;
using System.Threading.Tasks;
using HardwareScraper.Infrastructure.Services;

namespace HardwareScraper.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var scraper = new ScraperService();
                
                // Test single product scraping
                System.Console.WriteLine("Testing single product scraping...");
                var productUrl = "https://www.newegg.com/asrock-steel-legend-rx9070xt-sl-16g-amd-radeon-rx-9070-xt-16gb-gddr6/p/N82E16814930136";
                var productUrl2 = "https://www.newegg.com/asus-tuf-gaming-geforce-rtx-4070-ti-oc-edition-16gb-gddr7/p/N82E16814126655";
                var product = await scraper.ScrapeProductDetailsAsync(productUrl);
                
                System.Console.WriteLine("\nProduct Details:");
                System.Console.WriteLine($"Name: {product.Name}");
                System.Console.WriteLine($"Price: ${product.Price}");
                System.Console.WriteLine($"Brand: {product.Brand}");
                System.Console.WriteLine($"Category: {product.Category}");
                System.Console.WriteLine($"Image URL: {product.ImageUrl}");
                System.Console.WriteLine($"Product URL: {product.ProductUrl}");
                System.Console.WriteLine($"Description: {product.Description?.Substring(0, Math.Min(200, product.Description.Length))}...");

                // Test product list scraping
                System.Console.WriteLine("\nTesting product list scraping...");
                var searchUrl = "https://www.newegg.com/GPUs-Video-Graphics-Cards/SubCategory/ID-48";
                var products = await scraper.ScrapeProductsAsync(searchUrl);
                
                System.Console.WriteLine($"\nFound {products.Count()} products:");
                foreach (var p in products.Take(5)) // Show first 5 products
                {
                    System.Console.WriteLine($"\n- {p.Name}");
                    System.Console.WriteLine($"  Price: ${p.Price}");
                    System.Console.WriteLine($"  URL: {p.ProductUrl}");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
                System.Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

            System.Console.WriteLine("\nPress any key to exit...");
            System.Console.ReadKey();
        }
    }
} 