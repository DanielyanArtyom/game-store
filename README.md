# Game Store API

Capstone project from EPAM's **ASP.NET Development** training program: the
backend for an online game store, written in ASP.NET Core 8.

> This is a training project. The requirements and the Angular UI in
> `gamestore-ui-app/` (a prebuilt bundle) were provided by the program.
> The API, the data layer and the tests are my work.

## What it does

- Catalog of games, genres, platforms and publishers, with filtering, sorting and paging
- Comments with replies and quotes, and user bans
- Cart and orders, with three payment methods: bank invoice (generated as a PDF),
  IBox terminal and Visa (through an external payment service)
- Users and roles with JWT authentication and permission-based authorization
- A second data source: legacy product and shipper data in MongoDB (Northwind),
  synced with the main SQL Server catalog

## Solution structure

| Project | Responsibility |
|---|---|
| `GameStore.API` | Controllers, DTOs, middleware, auth setup, Serilog logging |
| `GameStore.Business` | Services, validation, mapping, payment clients |
| `GameStore.Data` | EF Core + SQL Server: context, migrations, repositories |
| `GameStore.Mongo.Data` | EF Core MongoDB provider: Northwind repositories |
| `GameStore.Data.Common` | Shared repository interfaces and search models |
| `GameStore.Tests` | xUnit + Moq unit tests for the service layer |

## Stack

C# · .NET 8 · ASP.NET Core Web API · EF Core (SQL Server, MongoDB) · AutoMapper ·
JWT · BCrypt · iText (PDF) · Serilog · Swagger · xUnit · Moq

## Running locally

1. Start SQL Server and MongoDB, and set both connection strings in
   `GameStore.API/appsettings.json`.
2. Apply migrations: `dotnet ef database update --project GameStore.Data --startup-project GameStore.API`
3. Run the API: `dotnet run --project GameStore.API` (Swagger UI is available in Development).
4. Run tests: `dotnet test`
