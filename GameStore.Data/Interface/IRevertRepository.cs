namespace GameStore.Data.Interface;

public interface IRevertRepository
{
    Task ResetCancelledOrderAsync(Order order, CancellationToken ct = default);
}