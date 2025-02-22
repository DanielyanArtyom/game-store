namespace GameStore.Business.Client;

public interface IPaymentClient
{
    Task BoxPaymentAsync(IBoxRequest request, CancellationToken cancellationToken = default);
    Task VisaPaymentAsync(VisaRequest request, CancellationToken cancellationToken = default);
}