namespace GameStore.Mongo.Data.Context.Entity;

public class Region: BaseEntity
{
    public required int RegionID { get; set; }
    public required string RegionDescription { get; set; }
}
