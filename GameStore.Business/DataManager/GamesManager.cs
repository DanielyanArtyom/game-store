using GameStore.Mongo.Data.Context.Entity;
using MongoDB.Bson;

namespace GameStore.Business.DataManager;

internal class GamesManager : IGamesManager
{
    private readonly IUnitOfWork _sqlUnitOfWork;
    private readonly IMongoUnitOfWork _noSqlUnitOfWork;
    private readonly ISyncService _syncService;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public GamesManager(
        IUnitOfWork sqlUnitOfWork,
        IMapper mapper,
        ISyncService syncWithMongoService,
        IMongoUnitOfWork noSqlUnitOfWork,
        IFileService fileService
        )
    {
        _sqlUnitOfWork = sqlUnitOfWork;
        _mapper = mapper;
        _syncService = syncWithMongoService;
        _noSqlUnitOfWork = noSqlUnitOfWork;
        _fileService = fileService;
    }

    #region READ methods. SQL Database + NoSQL Database

    public async Task<GameModel?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        if (Guid.TryParse(id, out Guid guidId))
        {
            var game = await _sqlUnitOfWork.Games.GetByIdAsync(guidId, ct);
            
            if (game is not null)
            {
                return _mapper.Map<GameModel>(game);
            }
        }

        if(ObjectId.TryParse(id, out ObjectId objectId))
        {
            var product = await _noSqlUnitOfWork.Products.GetByIdAsync(objectId, ct);
        
            if (product is null)
            {
                return null;
            }

            var syncedGame = await _syncService.SyncProductAsync(product, ct);
            
            return _mapper.Map<GameModel>(syncedGame);
        }

        throw new ArgumentException("Invalid id");
    }

    public async Task<GameModel?> GetByFilterAsync(GameSearchFilterModel filter, CancellationToken ct = default)
    {
        var gameFilter = filter.ToGameSearchContext();
        
        var game = (await _sqlUnitOfWork.Games.SearchAsync(gameFilter, ct)).Results.FirstOrDefault();
        
        if (game is not null)
        {
            return _mapper.Map<GameModel>(game);
        }

        var productFilter = filter.ToProductSearchContext();
        
        var product = (await _noSqlUnitOfWork.Products.SearchAsync(productFilter, ct)).Results.FirstOrDefault();
        
        if (product is null)
        {
            return null;
        }

        var syncedGame = await _syncService.SyncProductAsync(product, ct);
        return _mapper.Map<GameModel>(syncedGame);
    }

    public async Task<PagedGameModel> SearchAsync(GameSearchFilterModel filters, CancellationToken ct = default)
    {
        var gameFilter = filters.ToGameSearchContext();
        var gamesTask = _sqlUnitOfWork.Games.SearchAsync(gameFilter, ct);

        var productFilter = filters.ToProductSearchContext();
        var productsTask = _noSqlUnitOfWork.Products.SearchAsync(productFilter, ct);

        await Task.WhenAll(gamesTask, productsTask);

        var games = _mapper.Map<List<GameModel>>(gamesTask.Result.Results).Where(x => x.OriginalId is null).ToList();
        var products = _mapper.Map<List<GameModel>>(productsTask.Result.Results);

        var totalCount = gamesTask.Result.TotalCount + productsTask.Result.TotalCount;

        return new PagedGameModel
        {
            Games = games.Union(products).ToList(),
            CurrentPage = filters.PageNumber,
            TotalPages = (int)Math.Ceiling((double)totalCount / (int)filters.PageSize)
        };
    }

    public async Task<int> GetGamesCountAsync()
    {
        var gamesCountTask = _sqlUnitOfWork.Games.GetFilteredCountAsync(x => x.OriginalId == null);
        
        var productsCountTask = _noSqlUnitOfWork.Products.GetTotalCountAsync();

        await Task.WhenAll(gamesCountTask, productsCountTask);

        return gamesCountTask.Result + productsCountTask.Result;
    }

    public async Task<List<GameModel>> GetAllAsync()
    {
        var gamesTask = _sqlUnitOfWork.Games.SearchAsync(new SearchContext<Game>
        {
            Filter = x => x.OriginalId == null
        });
        
        var productsTask = _noSqlUnitOfWork.Products.SearchAsync(new SearchContext<Product>());

        await Task.WhenAll(gamesTask, productsTask);
        
        var games = _mapper.Map<List<GameModel>>(gamesTask.Result.Results).ToList();
        var products = _mapper.Map<List<GameModel>>(productsTask.Result.Results);

        return games.Union(products).ToList();

    }

    #endregion

    #region WRITE methods. SQL Database ONLY

    public Task CreateAsync(GameModel request, CancellationToken ct = default)
    {
        var game = _mapper.Map<Game>(request);
        
        game.PublishDate = DateTime.UtcNow;

        _sqlUnitOfWork.Games.Add(game);

        return  _sqlUnitOfWork.CompleteAsync(ct);
    }

    public Task UpdateAsync(GameModel request, CancellationToken ct = default)
    {
        var game = _mapper.Map<Game>(request);

       // game.GameGenres.Clear();
        //game.GamePlatforms.Clear();

        _sqlUnitOfWork.Games.Update(game.Id, game);

        return _sqlUnitOfWork.CompleteAsync(ct);
    }

    public Task DeleteAsync(Game game, CancellationToken ct = default)
    {
        _sqlUnitOfWork.Games.Delete(game);
        
        return _sqlUnitOfWork.CompleteAsync(ct);
    }

    public async Task IncrementViewsCountAsync(Game game, CancellationToken ct = default)
    {
        ++game.Views;
        
        await _sqlUnitOfWork.CompleteAsync(ct);
    }

    public Task<byte[]> DownladGameFileAsync(Game game, CancellationToken ct = default)
    {
        return _fileService.GenerateFileBytes(game);
    }

    #endregion
}
