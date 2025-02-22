using GameStore.Mongo.Data.Context.Entity;
using GameStore.Mongo.Data.Interface;
using GameStore.Mongo.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace GameStore.Mongo.Data.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddGameStoreMongoProvider(
        this IServiceCollection services, 
        string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        var mongoUrl = new MongoUrl(connectionString);
        
        var client = new MongoClient(connectionString);
        
        services.AddSingleton<IMongoClient>(_ => client);

        services.AddSingleton<IMongoDatabase>(_ => client.GetDatabase(mongoUrl.DatabaseName));
        
        RegisterRepository<Customer>(services, "customers");
        RegisterRepository<Category>(services, "categories");
        RegisterRepository<Employee>(services, "employees");
        RegisterRepository<EmployeeTerritory>(services, "employee-territories");
        RegisterRepository<OrderDetail>(services, "order-details");
        RegisterRepository<Order>(services, "orders");
        RegisterRepository<Product>(services, "products");
        RegisterRepository<Region>(services, "regions");
        RegisterRepository<Shipper>(services, "shippers");
        RegisterRepository<Supplier>(services, "suppliers");
        RegisterRepository<Territory>(services, "territories");
        
        services.AddScoped<IMongoUnitOfWork, UnitOfWork>();

        return services;
    }

    private static void RegisterRepository<TEntity>(
        IServiceCollection services, 
        string collectionName) where TEntity : BaseEntity
    {
        services.AddScoped<IRepository<TEntity>>(provider =>
        {
            var database = provider.GetRequiredService<IMongoDatabase>();
            return new BaseRepository<TEntity>(database, collectionName);
        });
    }
    
}