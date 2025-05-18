using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareScraper.Core.Entities;

namespace HardwareScraper.Core.Interfaces
{
    public interface IScraperService
    {
        Task<IEnumerable<Product>> ScrapeProductsAsync(string url);
        Task<Product> ScrapeProductDetailsAsync(string productUrl);
    }
} 