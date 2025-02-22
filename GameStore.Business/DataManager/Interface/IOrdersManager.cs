using GameStore.Mongo.Data.Context.Entity;
using Order = GameStore.Data.Context.Entity.Order;

namespace GameStore.Business.DataManager.Interface;

public interface IOrdersManager
{
    Task<OrderModel?> GetActiveOrderAsync(Guid customerId, CancellationToken ct = default);

    Task<OrderModel> GetOrderByIdAsync(string id, Guid customerId, CancellationToken ct = default);
    
    Task<List<OrderModel>> GetAllOrders( CancellationToken ct = default);

    Task AddOrderGameAsync(Game game, Order order, CancellationToken ct = default);

    Task RemoveOrderGameAsync(Order order, OrderGame orderGame, CancellationToken ct = default);
    
    Task<OrderModel> CreateActiveOrderAsync(Guid customerId, CancellationToken ct = default);
    
    Task<OrderGame>  UpdateOrderQuantity(Guid id, int count, CancellationToken ct = default);
    
    Task<OrderGame> RemoveOrderDetail(Guid id, CancellationToken ct = default);

}