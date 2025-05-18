using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HardwareScraper.Core.Interfaces;
using HardwareScraper.Core.Entities;

namespace HardwareScraper.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScraperController : ControllerBase
    {
        private readonly IScraperService _scraperService;
        private readonly IUnitOfWork _unitOfWork;

        public ScraperController(IScraperService scraperService, IUnitOfWork unitOfWork)
        {
            _scraperService = scraperService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("scrape-products")]
        public async Task<IActionResult> ScrapeProducts([FromBody] string url)
        {
            try
            {
                var products = await _scraperService.ScrapeProductsAsync(url);
                await _unitOfWork.Products.AddRangeAsync(products);
                await _unitOfWork.CompleteAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("scrape-product-details2")]
        public async Task<IActionResult> ScrapeProductDetails2([FromBody] string productUrl)
        {
            var product = await _scraperService.ScrapeProductDetailsAsync(productUrl);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Save the product to the database
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return Ok(product);
        }

        [HttpPost("scrape-product-details")]
        public async Task<IActionResult> ScrapeProductDetails([FromBody] string productUrl)
        {
            try
            {
                var product = await _scraperService.ScrapeProductDetailsAsync(productUrl);
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.CompleteAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return Ok(products);
        }
    }
} 