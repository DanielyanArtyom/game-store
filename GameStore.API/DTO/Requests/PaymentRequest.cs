namespace GameStore.API.DTO.Requests;

public class PaymentRequest
{
    [Required]
    public PaymentMethodEnum Method { get; set; }
    public PaymentCardModel? Card { get; set; }
}