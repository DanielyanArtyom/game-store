namespace GameStore.Data.Context.Entity;

public class Genre : BaseEntity
{
    public required string Name { get; set; }
    public Guid? ParentGenreId { get; set; }
    public Genre? ParentGenre { get; set; }
    
    public int? OriginalId { get; set; }
    
    public List<Genre> SubGenres { get; set; }
    public List<GameGenre> GameGenres { get; set; }
}