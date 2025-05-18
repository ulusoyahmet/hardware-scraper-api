using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardwareScraper.Core.Entities;
using HardwareScraper.Core.Interfaces;
using HardwareScraper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HardwareScraper.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
} 