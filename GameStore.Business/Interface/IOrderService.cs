namespace GameStore.Business.Interface;

public interface IOrderService
{
    Task RemoveGameFromCartAsync(string key, Guid customerId, CancellationToken ct = default);
    Task AddGameToCartAsync(string key, Guid customerId, CancellationToken ct = default);
    Task<OrderModel> GetByIdAsync(string id, Guid customerId);
    Task<List<OrderModel>> GetAllAsync();
    Task<List<OrderGameModel>> GetOrderDetailsAsync(Guid id, Guid customerId);
    Task<List<OrderGameModel>> GetOpenCartDetailsAsync(Guid customerId);
    Task UpdateOrderQuantity(Guid id, int count, CancellationToken ct = default);
    Task RemoveOrderDetail(Guid id, CancellationToken ct = default);
}