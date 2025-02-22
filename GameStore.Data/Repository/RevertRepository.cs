using GameStore.Data.Enum;

namespace GameStore.Data.Repository;

public class RevertRepository: IRevertRepository
{
    private readonly IDbContextFactory<GameStoreContext> _contextFactory;

    public RevertRepository(IDbContextFactory<GameStoreContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task ResetCancelledOrderAsync(Order order, CancellationToken ct = default)
    {
        using (var context = await _contextFactory.CreateDbContextAsync(ct))
        {
            foreach (var game in order.OrderGames)
            {
                var gameForReset = context.Games.FirstOrDefault(x => x.Id == game.ProductId);

                gameForReset.UnitInStock += game.Quantity;
                context.Games.Update(gameForReset);
            }

            order.Status = OrderStatus.Cancelled;
            order.OrderGames.Clear();

            context.Orders.Update(order);
            await context.SaveChangesAsync(ct);
        }
    }
}