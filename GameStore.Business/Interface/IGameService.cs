namespace GameStore.Business.Interface;

public interface IGameService : IBaseService<GameModel, GameModel>
{
    Task<GameModel> GetByKeyAsync(string key, CancellationToken ct = default);
    Task<int> GetGamesCountAsync();
    Task<PagedGameModel> GetGamesByPlatformIdAsync(Guid platformId, CancellationToken ct = default);
    Task<PagedGameModel> GetGamesByGenreIdAsync(Guid genreId, CancellationToken ct = default);
    Task<PagedGameModel> GetGamesByCompanyNameAsync(string companyName, CancellationToken ct = default);
    Task<byte[]> DownladGameFileAsync(string key, CancellationToken ct = default);
    Task<PagedGameModel> SearchAsync(GameSearchFilterModel filters, CancellationToken ct = default);
    Task DeleteGameAsync(string key, CancellationToken ct = default);
    Task IncrementViewCountAsync(string key);
    Task IncrementViewCountByIdAsync(Guid id);
}

