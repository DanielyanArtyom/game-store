namespace GameStore.Data.Context.Entity;

public class Platform : BaseEntity
{
    public required string Type { get; set; }
    public List<GamePlatform> GamePlatforms { get; set; }
}