namespace GameStore.Business.Model;

public class PublisherModel : BaseModel
{
    public string CompanyName { get; set; }
    public string? HomePage { get; set; }
    public string? Description { get; set; }
    
    public List<Game> Games { get; set; } = new();
}