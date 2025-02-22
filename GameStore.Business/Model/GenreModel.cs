namespace GameStore.Business.Model;

public class GenreModel : BaseModel
{
    public string Name { get; set; }
    public Guid? ParentGenreId { get; set; }
}