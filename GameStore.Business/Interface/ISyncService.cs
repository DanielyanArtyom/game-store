using GameStore.Mongo.Data.Context.Entity;
using MongoOrder = GameStore.Mongo.Data.Context.Entity.Order;
using Order = GameStore.Data.Context.Entity.Order;

namespace GameStore.Business.Interface;

public interface ISyncService
{
    Task<Game?> SyncProductAsync(Product product, CancellationToken ct = default);
    Task<Order?> SyncOrderAsync(MongoOrder order, Guid customerId, CancellationToken ct = default);
}