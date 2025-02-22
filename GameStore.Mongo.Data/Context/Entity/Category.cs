namespace GameStore.Mongo.Data.Context.Entity;

public class Category: BaseEntity
{
    public required int CategoryID { get; set; }
    public required string CategoryName { get; set; }
    public required string Description { get; set; }
    public required string Picture { get; set; }

}
