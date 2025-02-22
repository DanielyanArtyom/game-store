namespace GameStore.Mongo.Data.Context.Entity;

public class Territory: BaseEntity
{
    public required int TerritoryID { get; set; }
    public required string TerritoryDescription { get; set; }
    public required int RegionID { get; set; }
}
