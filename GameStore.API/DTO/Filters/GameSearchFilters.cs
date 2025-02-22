namespace GameStore.API.DTO.Requests;

public class GameSearchFilters
{
    public List<Guid>? GenreIds { get; set; }
    public List<Guid>? PlatformIds { get; set; }
    public List<Guid>? PublisherIds { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Name { get; set; }
    public PublishDateOption? PublishDate { get; set; }
    public SortingOption SortBy { get; set; } = SortingOption.New;
    public int PageNumber { get; set; } = 1;
    public PaginationOption PageSize { get; set; } = PaginationOption.Ten;
}