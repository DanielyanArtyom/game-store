namespace GameStore.Data.Context.Entity;

public class Game : BaseEntity
{
    public required string Name { get; set; }
    public required string Key { get; set; }
    public required decimal Price { get; set; }
    public required int UnitInStock { get; set; }
    public required decimal Discount { get; set; }
    public required DateTime PublishDate { get; set; }
    public required int Views { get; set; }
    public string? Description { get; set; }
    public int? OriginalId { get; set; }
    
    public required int ReorderLevel { get; set; }
    public required bool Discontinued { get; set; }
    public required string QuantityPerUnit { get; set; }
    public required int UnitsOnOrder { get; set; }
    
    public Guid PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    
    public List<GameGenre> GameGenres { get; set; } = new();
    public List<GamePlatform> GamePlatforms { get; set; } = new();
    public List<OrderGame> OrderGames { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}