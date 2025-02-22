using System.Text.Json.Serialization;

namespace GameStore.Business.Model.Filters;

public class GameSearchFilterModel
{
    public List<Guid>? GenreIds { get; set; }
    public List<Guid>? PlatformIds { get; set; }
    public List<string>? CompanyNames { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Name { get; set; }
    public PublishDateOption? PublishDate { get; set; }
    public SortingOption SortBy { get; set; } = SortingOption.New;
    public int PageNumber { get; set; } = 1;
    public PaginationOption PageSize { get; set; } = PaginationOption.Ten;
    
    [JsonIgnore]
    public string Key { get; set; }
}