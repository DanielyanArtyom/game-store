namespace GameStore.Business.DataManager.Interface;

public interface IGamesManager
{
    #region READ methods. SQL Database + NoSQL Database

    Task<GameModel?> GetByIdAsync(string id, CancellationToken ct = default);

    Task<GameModel?> GetByFilterAsync(GameSearchFilterModel filter, CancellationToken ct = default);

    Task<PagedGameModel> SearchAsync(GameSearchFilterModel filters, CancellationToken ct = default);

    Task<int> GetGamesCountAsync();

    Task<List<GameModel>> GetAllAsync();

    #endregion

    #region WRITE methods. SQL Database ONLY

    Task CreateAsync(GameModel request, CancellationToken ct = default);

    Task UpdateAsync(GameModel request, CancellationToken ct = default);

    Task DeleteAsync(Game game, CancellationToken ct = default);

    Task IncrementViewsCountAsync(Game game, CancellationToken ct = default);

    Task<byte[]> DownladGameFileAsync(Game game, CancellationToken ct = default);

    #endregion
}
