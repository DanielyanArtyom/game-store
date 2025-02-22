namespace GameStore.Business.Model;

public class IBoxPaymentResultModel
{
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal Sum { get; set; }
    public byte[]? PdfInvoice { get; set; }
}