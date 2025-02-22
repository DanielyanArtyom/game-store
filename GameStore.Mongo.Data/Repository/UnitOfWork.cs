using GameStore.Mongo.Data.Context.Entity;
using GameStore.Mongo.Data.Interface;

namespace GameStore.Mongo.Data.Repository;

public class UnitOfWork: IMongoUnitOfWork
{
    public UnitOfWork(
        IRepository<Customer> customerRepository,
        IRepository<Category> categoryRepository,
        IRepository<Employee> employeeRepository,
        IRepository<EmployeeTerritory> empTerrRepository,
        IRepository<Order> orderRepository,
        IRepository<OrderDetail> orderDetailRepository,
        IRepository<Product> productRepository,
        IRepository<Region> regionRepository,
        IRepository<Shipper> shipperRepository,
        IRepository<Supplier> supplierRepository,
        IRepository<Territory> territoryRepository)
    {
        Customers = customerRepository;
        Categories = categoryRepository;
        Employees = employeeRepository;
        EmployeeTerritories = empTerrRepository;
        Orders = orderRepository;
        OrderDetails = orderDetailRepository;
        Products = productRepository;
        Regions = regionRepository;
        Shippers = shipperRepository;
        Suppliers = supplierRepository;
        Territories = territoryRepository;
    }
    
    public IRepository<Customer> Customers { get; set; }
    public IRepository<Category> Categories { get; set; }
    public IRepository<Employee> Employees { get; set; }
    public IRepository<EmployeeTerritory> EmployeeTerritories { get; set; }
    public IRepository<Order> Orders { get; set; }
    public IRepository<OrderDetail> OrderDetails { get; set; }
    public IRepository<Product> Products { get; set; }
    public IRepository<Region> Regions { get; set; }
    public IRepository<Shipper> Shippers { get; set; }
    public IRepository<Supplier> Suppliers { get; set; }
    public IRepository<Territory> Territories { get; set; }
    
    public Task CompleteAsync(CancellationToken ct = default)
    {
        return Task.CompletedTask;
    }

    public void Dispose(){}
}
  