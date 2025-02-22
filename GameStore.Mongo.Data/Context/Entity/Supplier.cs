namespace GameStore.Mongo.Data.Context.Entity;

public class Supplier: BaseEntity
{
    public required int SupplierID { get; set; }
    public required string CompanyName { get; set; }
    public required string ContactName { get; set; }
    public required string ContactTitle { get; set; }
    public object Address { get; set; }
    public object City { get; set; }
    public object? Region { get; set; }
    public object PostalCode { get; set; }
    public object Country { get; set; }
    public object Phone { get; set; }
    public object? Fax { get; set; }
    public object? HomePage { get; set; }
}
