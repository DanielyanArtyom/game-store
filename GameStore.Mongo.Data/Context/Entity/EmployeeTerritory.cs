namespace GameStore.Mongo.Data.Context.Entity;

public class EmployeeTerritory: BaseEntity
{
    public required int EmployeeID { get; set; }
    public required int TerritoryID { get; set; }
}
