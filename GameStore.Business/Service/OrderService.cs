namespace GameStore.Business.Service;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrdersManager _ordersManager;
    private readonly IGamesManager _gamesManager;
    private readonly ITransactionsManager _transactionsManager;

    public OrderService(IOrdersManager ordersManager, IGamesManager gamesManager , ITransactionsManager transactionManager ,IMapper mapper)
    {
        _ordersManager = ordersManager;
        _mapper = mapper;
        _gamesManager = gamesManager;
        _transactionsManager = transactionManager;
    }

    public async Task AddGameToCartAsync(string key, Guid customerId, CancellationToken ct = default)
    {
        try
        {
            await _transactionsManager.BeginTransactionAsync(ct);
            
            var gameTask = _gamesManager.GetByFilterAsync(new GameSearchFilterModel { Key = key }, ct);

            var orderTask = _ordersManager.GetActiveOrderAsync(customerId, ct);

            await Task.WhenAll(gameTask, orderTask);

            var order = orderTask.Result;

            var game = gameTask.Result;

            if (game == null)
            {
                throw new NotFoundException("Game not found");
            }

            if (order == null)
            {
                order = await _ordersManager.CreateActiveOrderAsync(customerId, ct);
            }

            if (game != null && game.UnitInStock == 0)
            {
                throw new ArgumentException("Product out of stock");
            }

            --game.UnitInStock;

            await _gamesManager.UpdateAsync(game, ct);

            await _ordersManager.AddOrderGameAsync(_mapper.Map<Game>(game), _mapper.Map<Order>(order), ct);

            await _transactionsManager.CommitTransactionAsync(ct);
        }
        catch
        {
            await _transactionsManager.RollbackTransactionAsync(ct);
            throw;
        }
    }
    
    public async Task RemoveGameFromCartAsync(string key, Guid customerId, CancellationToken ct = default)
    {
        try
        {
            await _transactionsManager.BeginTransactionAsync(ct);
            
            var gameTask = _gamesManager.GetByFilterAsync(new GameSearchFilterModel { Key = key }, ct);
            
            var orderTask = _ordersManager.GetActiveOrderAsync(customerId, ct);

            await Task.WhenAll(gameTask, orderTask);

            var order = orderTask.Result;
            
            var game = gameTask.Result;
            
            if (game == null)
            {
                throw new NotFoundException("Game not found");
            }

            if (order == null)
            {
                throw new NotFoundException("Cart not found");
            }

            ++game.UnitInStock;

            await _gamesManager.UpdateAsync(game, ct);

            var orderGame = order.OrderGames.FirstOrDefault(og => og.ProductId == game.Id);

            if (orderGame == null)
            {
                throw new NotFoundException("Game not found in the cart.");
            }

            await _ordersManager.RemoveOrderGameAsync(_mapper.Map<Order>(order), orderGame, ct);
            await _transactionsManager.CommitTransactionAsync(ct);
        }
        catch
        {
            await _transactionsManager.RollbackTransactionAsync(ct);
            throw;
        }
    }

    public async Task<OrderModel> GetByIdAsync(string id, Guid customerId)
    {
        var order = await _ordersManager.GetOrderByIdAsync(id, customerId);

        if (order == null)
        {
            throw new NotFoundException("Order is not found");
        }

        return order;
    }

    public Task<List<OrderModel>> GetAllAsync()
    {
        return _ordersManager.GetAllOrders();
    }
    
    public async Task<List<OrderGameModel>> GetOrderDetailsAsync(Guid id, Guid customerId)
    {
        var result = await _ordersManager.GetOrderByIdAsync(id.ToString(), customerId);

        if (result == null)
        {
            throw new NotFoundException("Order is not found");
        }

        return _mapper.Map<List<OrderGameModel>>(result.OrderGames);
    }
    
    public async Task<List<OrderGameModel>> GetOpenCartDetailsAsync(Guid customerId)
    {
        var existingCart = await _ordersManager.GetActiveOrderAsync(customerId);

        if (existingCart == null)
        {
            throw new NotFoundException("Cart is not found");
        }

        return _mapper.Map<List<OrderGameModel>>(existingCart.OrderGames);
    }

    public async Task UpdateOrderQuantity(Guid id, int count, CancellationToken ct = default)
    {
        if (count <= 0)
        {
            throw new ArgumentException("Count could not be less or equal to zero");
        }

        var orderGame = await _ordersManager.UpdateOrderQuantity(id, count, ct);

        if (orderGame == null)
        {
            throw new NotFoundException("Order details are not found");
        }
    }

    public  async Task RemoveOrderDetail(Guid id, CancellationToken ct = default)
    {
        var orderDetail = await _ordersManager.RemoveOrderDetail(id, ct);

        if (orderDetail == null)
        {
            throw new NotFoundException("Order detail in not found");
        }
    }
}