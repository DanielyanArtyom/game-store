namespace GameStore.Mongo.Data.Context.Entity;

public class Order : BaseEntity
{
    public required int OrderID { get; set; }
    public required string CustomerID { get; set; }
    public required int EmployeeID { get; set; }
    public required string OrderDate { get; set; }
    public required string RequiredDate { get; set; }
    public required string ShippedDate { get; set; }
    public required int ShipperID { get; set; }
    public required decimal Freight { get; set; }
    public required string ShipName { get; set; }
    public object ShipAddress { get; set; }
    
    public object ShipCity { get; set; }
    public string? ShipRegion { get; set; }
    
    public object ShipPostalCode { get; set; }
    public object ShipCountry { get; set; }
    public object ShipVia { get; set; }
}
