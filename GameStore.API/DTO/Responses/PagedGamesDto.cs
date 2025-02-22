namespace GameStore.API.DTO.Responses;

public class PagedGamesDto
{
    public required List<GameDto> Games { get; set; }
    public required int CurrentPage { get; set; }
    public required int TotalPages { get; set; }
}