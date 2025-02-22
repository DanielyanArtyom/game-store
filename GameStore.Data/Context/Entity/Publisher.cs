namespace GameStore.Data.Context.Entity;

public class Publisher : BaseEntity
{
    public string CompanyName { get; set; }
    public string? HomePage { get; set; }
    public string? Description { get; set; }
    
    public List<Game> Games { get; set; } = new();
}