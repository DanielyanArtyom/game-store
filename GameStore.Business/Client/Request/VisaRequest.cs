namespace GameStore.Business.Client.Request;

public class VisaRequest
{
    public required decimal TransactionAmount { get; set; }
    public required string CardHolderName { get; set; }
    public required string CardNumber { get; set; }
    public required int ExpirationMonth { get; set; }
    public required int ExpirationYear { get; set; }
    public required int Cvv { get; set; }
}