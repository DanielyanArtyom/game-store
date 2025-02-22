namespace GameStore.Business.Service;

public class GameService : IGameService
{
    private readonly IVisitor _visitor;
    private readonly IGamesManager _gamesManager;
    private readonly IMapper _mapper;

    public GameService(IGamesManager gamesManager, IMapper mapper ,IVisitor visitor)
    {
        _visitor = visitor;
        _gamesManager = gamesManager;
        _mapper = mapper;
    }

    public async Task CreateAsync(GameModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);

        var existingGame = await _gamesManager.GetByFilterAsync(new GameSearchFilterModel
        {
            Key = request.Key ?? request.Name
        }, ct);

        if (existingGame is not null)
        {
            throw new DuplicateFoundException("Game is already exists");
        }
        
        await _gamesManager.CreateAsync(request, ct);
    }

    public async Task UpdateAsync(GameModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);
        
        var existingGame = await _gamesManager.GetByFilterAsync(new GameSearchFilterModel
        {
            Key = request.Key ?? request.Name
        }, ct);

        if (existingGame is null)
        {
            throw new NotFoundException("Game Not Found");
        }

        if (existingGame != null && existingGame.Id != request.Id)
        {
            throw new DuplicateFoundException("Game with this key already exist");
        }
        
        await _gamesManager.UpdateAsync(request, ct);
    }

    public Task<GameModel> GetByKeyAsync(string key, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        var filter = new GameSearchFilterModel { Key = key };

        return _gamesManager.GetByFilterAsync(filter, ct);
    }

    public Task<int> GetGamesCountAsync()
    {
        return _gamesManager.GetGamesCountAsync();
    }

    public Task<GameModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _gamesManager.GetByIdAsync(id.ToString(), ct);
    }

    public Task<PagedGameModel> GetGamesByPlatformIdAsync(Guid platformId, CancellationToken ct = default)
    {
        var filter = new GameSearchFilterModel
        {
            PlatformIds = new List<Guid> { platformId }
        };
        
        return _gamesManager.SearchAsync(filter, ct);
    }

    public Task<PagedGameModel> GetGamesByGenreIdAsync(Guid genreId, CancellationToken ct = default)
    {
        var filter = new GameSearchFilterModel
        {
            GenreIds = new List<Guid> { genreId }
        };
        
        return _gamesManager.SearchAsync(filter, ct);
    }

    public Task<PagedGameModel> GetGamesByCompanyNameAsync(string companyName, CancellationToken ct = default)
    {
        var filter = new GameSearchFilterModel
        {
            CompanyNames = new List<string>{ companyName }
        };
        
        return _gamesManager.SearchAsync(filter, ct);
    }

    public async Task<byte[]> DownladGameFileAsync(string key, CancellationToken ct = default)
    {
        
        var game = await GetByKeyAsync(key, ct);

        if (game is null)
        {
            throw new NotFoundException("Game not found");
        }
        
        return await _gamesManager.DownladGameFileAsync(_mapper.Map<Game>(game), ct);
    }

    public Task<PagedGameModel> SearchAsync(GameSearchFilterModel filters, CancellationToken ct = default)
    {
        return _gamesManager.SearchAsync(filters, ct);
    }

    public async Task DeleteGameAsync(string key, CancellationToken ct = default)
    {
        var game = await GetByKeyAsync(key, ct);

        if (game is null)
        {
            throw new NotFoundException("Game not found");
        }

        await _gamesManager.DeleteAsync(_mapper.Map<Game>(game), ct);
    }

    public async Task IncrementViewCountAsync(string key)
    {
        var game = await GetByKeyAsync(key);
        
        if (game == null)
        {
            throw new NotFoundException("Game does not exists");
        }
        
        await _gamesManager.IncrementViewsCountAsync(_mapper.Map<Game>(game));
    }

    public async Task IncrementViewCountByIdAsync(Guid id)
    {
        var game = await GetByIdAsync(id);
        
        if (game == null)
        {
            throw new NotFoundException("Game does not exists");
        }
        
        await _gamesManager.IncrementViewsCountAsync(_mapper.Map<Game>(game));
    }

    public Task<List<GameModel>> GetAllAsync()
    {
        return _gamesManager.GetAllAsync();
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var game = await _gamesManager.GetByIdAsync(id.ToString(), ct);

        if (game == null)
        {
            throw new NotFoundException("Game not found");
        }
        
        await _gamesManager.DeleteAsync(_mapper.Map<Game>(game), ct);
    }
}
