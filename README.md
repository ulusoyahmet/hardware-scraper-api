# Hardware Scraper API

A web scraping API built with ASP.NET Core 8.0 that follows Onion architecture, Repository pattern, and Unit of Work pattern.

## Project Structure

The solution is organized into the following projects:

- **HardwareScraper.API**: The Web API project that handles HTTP requests
- **HardwareScraper.Core**: Contains domain entities, interfaces, and business logic
- **HardwareScraper.Infrastructure**: Implements data access, repositories, and external services
- **HardwareScraper.Application**: Contains application services and business logic (if needed)

## Features

- Web scraping of hardware products
- Product details extraction
- Data persistence using Entity Framework Core
- Repository pattern implementation
- Unit of Work pattern for transaction management
- RESTful API endpoints
- Swagger documentation

## Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB or full version)
- Visual Studio 2022 or Visual Studio Code

## Getting Started

1. Clone the repository
2. Update the connection string in `appsettings.json` if needed
3. Open the solution in Visual Studio
4. Restore NuGet packages
5. Run the following commands in the Package Manager Console:
   ```
   Add-Migration InitialCreate
   Update-Database
   ```
6. Run the application

## API Endpoints(url: newegg for now)

- `POST /api/scraper/scrape-products`: Scrape products from a given URL
- `POST /api/scraper/scrape-product-details`: Scrape detailed information for a specific product
- `GET /api/scraper/products`: Get all scraped products

## Architecture

The project follows clean architecture principles:

- **Domain Layer** (Core): Contains business entities and interfaces
- **Infrastructure Layer**: Implements data access and external services
- **Application Layer**: Contains business logic and use cases
- **Presentation Layer** (API): Handles HTTP requests and responses

## Patterns Used

- **Repository Pattern**: Abstracts data access logic
- **Unit of Work Pattern**: Manages transactions and ensures data consistency
- **Dependency Injection**: For loose coupling and better testability

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License. 
