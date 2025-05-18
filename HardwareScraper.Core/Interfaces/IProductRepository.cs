using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareScraper.Core.Entities;

namespace HardwareScraper.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
        Task<Product> GetByIdAsync(int id);
    }
} 