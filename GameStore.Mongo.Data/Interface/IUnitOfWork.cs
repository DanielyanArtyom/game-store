using GameStore.Data.Common.Interface;
using GameStore.Mongo.Data.Context.Entity;

namespace GameStore.Mongo.Data.Interface;

public interface IMongoUnitOfWork: IBaseUnitOfWork
{
    IRepository<Customer> Customers { get; set; }
    IRepository<Category> Categories { get; set; }
    IRepository<Employee> Employees { get; set; }
    IRepository<EmployeeTerritory> EmployeeTerritories { get; set; }
    IRepository<Order> Orders { get; set; }
    IRepository<OrderDetail> OrderDetails { get; set; }
    IRepository<Product> Products { get; set; }
    IRepository<Region> Regions { get; set; }
    IRepository<Shipper> Shippers { get; set; }
    IRepository<Supplier> Suppliers { get; set; }
    IRepository<Territory> Territories { get; set; }
}