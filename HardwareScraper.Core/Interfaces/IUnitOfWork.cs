using System;
using System.Threading.Tasks;
using HardwareScraper.Core.Entities;

namespace HardwareScraper.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        Task<int> CompleteAsync();
    }
} 