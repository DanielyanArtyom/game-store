namespace GameStore.API.DTO.Responses;

public class PublisherDto : BaseDto
{
    public string CompanyName { get; set; }
    public string? HomePage { get; set; }
    public string? Description { get; set; }
}