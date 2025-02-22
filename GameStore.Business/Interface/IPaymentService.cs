namespace GameStore.Business.Interface;

public interface IPaymentService
{
    Task<List<PaymentMethodModel>> GetAllAsync();
}