namespace GameStore.Business.DependencyInjection;

public class PaymentServiceOptions
{
    public required int OrderValidityInMonths { get; set; }
    public required string PaymentServiceUrl { get; set; }
    public required string VisaUrl { get; set; }
    public required string IBoxUrl { get; set; }
}