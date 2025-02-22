using GameStore.Mongo.Data.Context.Entity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace GameStore.Mongo.Data.Context;

public class GameStoreMongoContext : DbContext
{
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Category> Categories { get; init; }
    public DbSet<Employee> Employees { get; init; }
    public DbSet<EmployeeTerritory> EmployeeTerritories { get; init; }
    public DbSet<Order> Orders { get; init; }
    public DbSet<OrderDetail> OrderDetails { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<Region> Regions { get; init; }
    public DbSet<Shipper> Shippers { get; init; }
    public DbSet<Supplier> Suppliers { get; init; }
    public DbSet<Territory> Territories { get; init; }

    public static GameStoreMongoContext Create(IMongoDatabase database)
    {
        var options = new DbContextOptionsBuilder<GameStoreMongoContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options;

        return new GameStoreMongoContext(options);
    }

    public GameStoreMongoContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>().ToCollection("customers");
        modelBuilder.Entity<Category>().ToCollection("categories");
        modelBuilder.Entity<Employee>().ToCollection("employees");
        modelBuilder.Entity<EmployeeTerritory>().ToCollection("employee-territories");
        modelBuilder.Entity<OrderDetail>().ToCollection("order-details");
        modelBuilder.Entity<Order>().ToCollection("orders");
        modelBuilder.Entity<Product>().ToCollection("products");
        modelBuilder.Entity<Region>().ToCollection("regions");
        modelBuilder.Entity<Shipper>().ToCollection("shippers");
        modelBuilder.Entity<Supplier>().ToCollection("suppliers");
        modelBuilder.Entity<Territory>().ToCollection("territories");
    }
}