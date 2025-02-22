namespace GameStore.API.DTO.Responses;

public class PaymentMethodDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
}