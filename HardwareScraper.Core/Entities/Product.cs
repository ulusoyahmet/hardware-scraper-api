using System;

namespace HardwareScraper.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public string Brand { get; set; } = "";
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string? ProductUrl { get; set; } = string.Empty;
        public DateTime? ScrapedAt { get; set; } = DateTime.UtcNow;
        public string? Source { get; set; } = "";
    }
} 