using System;
using System.Threading.Tasks;
using HardwareScraper.Core.Entities;
using HardwareScraper.Core.Interfaces;
using HardwareScraper.Infrastructure.Data;
using HardwareScraper.Infrastructure.Repositories;

namespace HardwareScraper.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Product> _products;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Product> Products => _products ??= new Repository<Product>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
} 