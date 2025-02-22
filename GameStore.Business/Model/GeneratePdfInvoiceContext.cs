namespace GameStore.Business.Model;

public class GeneratePdfInvoiceContext
{
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
    public decimal Sum { get; set; }
    public int ExpireAfterMonths { get; set; }
}