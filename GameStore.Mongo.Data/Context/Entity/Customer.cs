namespace GameStore.Mongo.Data.Context.Entity;

public class Customer: BaseEntity
{
    public required string CustomerID { get; set; }
    public required string CompanyName { get; set; }
    public required string ContactName { get; set; }
    public required string ContactTitle { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string Region { get; set; }
    public object PostalCode { get; set; }
    public required string Country { get; set; }
    public required string Phone { get; set; }
    public required string Fax { get; set; }
}
