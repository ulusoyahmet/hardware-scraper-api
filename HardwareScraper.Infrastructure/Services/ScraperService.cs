using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HardwareScraper.Core.Entities;
using HardwareScraper.Core.Interfaces;

namespace HardwareScraper.Infrastructure.Services
{
    public class ScraperService : IScraperService
    {
        private readonly HtmlWeb _web;

        public ScraperService()
        {
            _web = new HtmlWeb();
        }

        public async Task<IEnumerable<Product>> ScrapeProductsAsync(string url)
        {
            var products = new List<Product>();
            var doc = await Task.Run(() => _web.Load(url));

            // to be updated...
            var productNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'product-item')]");

            if (productNodes != null)
            {
                foreach (var node in productNodes)
                {
                    var product = new Product
                    {
                        Name = node.SelectSingleNode(".//h2")?.InnerText.Trim(),
                        Price = ParsePrice(node.SelectSingleNode(".//span[contains(@class, 'price')]")?.InnerText),
                        ImageUrl = node.SelectSingleNode(".//img")?.GetAttributeValue("src", ""),
                        ProductUrl = node.SelectSingleNode(".//a")?.GetAttributeValue("href", ""),
                        ScrapedAt = DateTime.UtcNow,
                        Source = new Uri(url).Host
                    };

                    if (!string.IsNullOrEmpty(product.Name))
                    {
                        products.Add(product);
                    }
                }
            }

            return products;
        }

        public async Task<Product> ScrapeProductDetailsAsync(string productUrl)
        {
            var doc = await Task.Run(() => _web.Load(productUrl));

            // selectors 
            var product = new Product
            {
                Name = doc.DocumentNode.SelectSingleNode("//h1[@class='product-title']")?.InnerText.Trim(),
                Description = doc.DocumentNode.SelectSingleNode("//div[@id='product-details']//div[@class='product-description']")?.InnerText.Trim() ?? "No description available",
                Price = ParsePrice(doc.DocumentNode.SelectSingleNode("//div[@class='price-current']")?.InnerText),
                Brand = doc.DocumentNode.SelectSingleNode("//div[@class='breadcrumbs']//li[last()-1]/a")?.InnerText.Trim(),
                Category = doc.DocumentNode.SelectSingleNode("//div[@class='breadcrumbs']//li[contains(@class, 'is-active')]/preceding-sibling::li[2]/a")?.InnerText.Trim(),
                ImageUrl = doc.DocumentNode.SelectSingleNode("//div[@class='swiper-zoom-container']//img[@class='product-view-img-original']")?.GetAttributeValue("src", ""),
                ProductUrl = productUrl,
                ScrapedAt = DateTime.UtcNow,
                Source = new Uri(productUrl).Host
            };

            return product;
        }

        private decimal ParsePrice(string priceText)
        {
            if (string.IsNullOrEmpty(priceText))
                return 0;

            // Extract the price 
            var priceMatch = System.Text.RegularExpressions.Regex.Match(priceText, @"\$(\d+)\.(\d+)");
            if (priceMatch.Success)
            {
                var wholePart = priceMatch.Groups[1].Value;
                var decimalPart = priceMatch.Groups[2].Value;
                var fullPrice = $"{wholePart},{decimalPart}";

                if (decimal.TryParse(fullPrice, out decimal price))
                    return price;
            }

            return 0;
        }
    }
} 