namespace GameStore.Mongo.Data.Context.Entity;

public class Shipper: BaseEntity
{ 
    public required int ShipperID { get; set; }
    public required string CompanyName { get; set; }
    public required string Phone { get; set; }
}
