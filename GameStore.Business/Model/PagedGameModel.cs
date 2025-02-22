namespace GameStore.Business.Model;

public class PagedGameModel
{
    public required List<GameModel> Games { get; set; }
    public required int CurrentPage { get; set; }
    public required int TotalPages { get; set; }
}