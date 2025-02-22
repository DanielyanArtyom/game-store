namespace GameStore.Business.Client.Request;

public class IBoxRequest
{
    public required Guid AccountNumber { get; set; }
    public required Guid InvoiceNumber { get; set; }
    public required decimal TransactionAmount { get; set; }
}