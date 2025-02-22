namespace GameStore.Data.Repository;

public class UserRespository: BaseRepository<User>, IUserRespository
{
    private readonly IDbContextFactory<GameStoreContext> _contextFactory;
    
    public UserRespository(IDbContextFactory<GameStoreContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<User> GetUserFullDataAsync(string login, CancellationToken ct = default)
    {
        using (var context = await _contextFactory.CreateDbContextAsync(ct))
        {
            return await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(r => r.Permissions)
                .FirstOrDefaultAsync(u => u.Login == login, ct);
        }
    }
}
