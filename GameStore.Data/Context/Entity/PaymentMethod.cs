namespace GameStore.Data.Context.Entity;

public class PaymentMethod: BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
}