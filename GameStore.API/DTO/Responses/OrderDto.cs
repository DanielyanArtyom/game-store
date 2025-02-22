namespace GameStore.API.DTO.Responses;

public class OrderDto : BaseDto
{
    public required DateTime? Date { get; set; }
    public required Guid CustomerId { get; set; }
}