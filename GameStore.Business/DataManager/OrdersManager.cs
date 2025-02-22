using GameStore.Mongo.Data.Context.Entity;
using MongoDB.Bson;
using Order = GameStore.Data.Context.Entity.Order;

namespace GameStore.Business.DataManager;

public class OrdersManager: IOrdersManager
{
    private readonly IUnitOfWork _sqlUnitOfWork;
    private readonly IMongoUnitOfWork _noSqlUnitOfWork;
    private readonly ISyncService _syncService;
    private readonly IMapper _mapper;

    public OrdersManager(
        IUnitOfWork sqlUnitOfWork,
        IMapper mapper,
        ISyncService syncWithMongoService,
        IMongoUnitOfWork noSqlUnitOfWork)
    {
        _sqlUnitOfWork = sqlUnitOfWork;
        _mapper = mapper;
        _syncService = syncWithMongoService;
        _noSqlUnitOfWork = noSqlUnitOfWork;
    }
    
    public async Task<OrderModel?> GetActiveOrderAsync(Guid customerId, CancellationToken ct = default)
    {
        var existingCart = (await _sqlUnitOfWork.Orders.SearchAsync(new SearchContext<Order>
        {
            Filter = x => x.Status == OrderStatus.Open && x.CustomerId == customerId,
            Include = new Expression<Func<Order, object>>[] { x => x.OrderGames }
        }, ct)).Results.FirstOrDefault();

        return _mapper.Map<OrderModel>(existingCart);
    }   

    public async Task<OrderModel> GetOrderByIdAsync(string id, Guid customerId ,CancellationToken ct = default)
    {
        if (Guid.TryParse(id, out Guid guidId))
        {
            var game = await _sqlUnitOfWork.Orders.GetByIdAsync(guidId, ct);
            
            if (game is not null)
            {
                return _mapper.Map<OrderModel>(game);
            }
        }

        if(ObjectId.TryParse(id, out ObjectId objectId))
        {
            var product = await _noSqlUnitOfWork.Orders.GetByIdAsync(objectId, ct);
        
            if (product is null)
            {
                return null;
            }

            var syncedGame = await _syncService.SyncOrderAsync(product, customerId, ct);
            
            return _mapper.Map<OrderModel>(syncedGame);
        }

        throw new ArgumentException("Invalid id");
    }

    public async Task<List<OrderModel>> GetAllOrders(CancellationToken ct = default)
    {
        var sqlOrders =
            _sqlUnitOfWork.Orders.SearchAsync(new SearchContext<Order> { Filter = x => x.OriginalId == null }, ct);

        var mongoOrders = _noSqlUnitOfWork.Orders.SearchAsync(new SearchContext<Mongo.Data.Context.Entity.Order>(), ct);

        await Task.WhenAll(sqlOrders, mongoOrders);

        var mappedOrders = _mapper.Map<List<OrderModel>>(sqlOrders.Result.Results);
        var mappedMongoOrders = _mapper.Map<List<OrderModel>>(mongoOrders.Result.Results);

        return mappedOrders.Union(mappedMongoOrders).ToList();
    }

    public Task AddOrderGameAsync(Game game, Order order, CancellationToken ct = default)
    {
        var orderGame = order.OrderGames.FirstOrDefault(og => og.ProductId == game.Id);

        if (orderGame != null)
        {
            orderGame.Quantity += 1;
            _sqlUnitOfWork.OrderGames.Update(orderGame.Id, orderGame);
        }
        else
        {
            orderGame = new OrderGame
            {
                OrderId = order.Id,
                ProductId = game.Id,
                Price = game.Price,
                Quantity = 1,
                Discount = game.Discount,
            };
            _sqlUnitOfWork.OrderGames.Add(orderGame);
        }
        return _sqlUnitOfWork.CompleteAsync(ct);         
    }

    public Task RemoveOrderGameAsync(Order order, OrderGame orderGame, CancellationToken ct = default)
    {
        --orderGame.Quantity;

        if (orderGame.Quantity <= 0)
        {
            _sqlUnitOfWork.OrderGames.Delete(orderGame);
        }
        else
        {
            _sqlUnitOfWork.OrderGames.Update(orderGame.Id, orderGame);
        }

        if (!order.OrderGames.Any())
        {
            _sqlUnitOfWork.Orders.Delete(_mapper.Map<Order>(order));
        }

        return _sqlUnitOfWork.CompleteAsync(ct);       
    }
    
    public async Task<OrderModel>CreateActiveOrderAsync(Guid customerId, CancellationToken ct = default)
    {
        var order = new Order
        {
            Date = DateTime.UtcNow,
            CustomerId = customerId,
            Status = OrderStatus.Open,
            OrderGames = new List<OrderGame>(),
            OriginalId = null,
        };
            
        _sqlUnitOfWork.Orders.Add(order);
            
        await _sqlUnitOfWork.CompleteAsync(ct);

        return _mapper.Map<OrderModel>(order);
    }

    public async Task<OrderGame> UpdateOrderQuantity(Guid id, int count, CancellationToken ct = default)
    {
        var orderDetail = await _sqlUnitOfWork.OrderGames.GetByIdAsync(id, ct);

        if (orderDetail == null)
        {
            return null;
        }

        orderDetail.Quantity += count;
        
        _sqlUnitOfWork.OrderGames.Update(id, orderDetail);

        await _sqlUnitOfWork.CompleteAsync(ct);

        return orderDetail;
    }

    public async Task<OrderGame> RemoveOrderDetail(Guid id, CancellationToken ct = default)
    {
        var orderDetail = await _sqlUnitOfWork.OrderGames.GetByIdAsync(id, ct);

        if (orderDetail == null)
        {
            return null;
        }

        _sqlUnitOfWork.OrderGames.DeleteById(id);

        return orderDetail;
    }
}