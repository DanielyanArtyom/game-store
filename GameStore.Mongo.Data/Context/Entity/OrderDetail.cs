namespace GameStore.Mongo.Data.Context.Entity;

public class OrderDetail: BaseEntity
{
    public required int OrderID { get; set; }
    public required int ProductID { get; set; }
    public required decimal UnitPrice { get; set; }
    public required int Quantity { get; set; }
    public required float Discount { get; set; }
}
