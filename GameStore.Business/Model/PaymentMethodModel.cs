namespace GameStore.Business.Model;


public class PaymentMethodModel: BaseModel
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
}