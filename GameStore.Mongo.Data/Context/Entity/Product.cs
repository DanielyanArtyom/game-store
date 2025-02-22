namespace GameStore.Mongo.Data.Context.Entity;

public class Product: BaseEntity
{
    public required int ProductID { get; set; }
    public required string ProductName { get; set; }
    public required int SupplierID { get; set; }
    public required int CategoryID { get; set; }
    
    public required decimal UnitPrice { get; set; }
    public required int UnitsInStock { get; set; }
    public required int UnitsOnOrder { get; set; }
    public required int ReorderLevel { get; set; }
    public required bool Discontinued { get; set; }
    public required string QuantityPerUnit { get; set; }
}
