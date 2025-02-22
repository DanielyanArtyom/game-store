using Microsoft.EntityFrameworkCore.Storage;

namespace GameStore.Data.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbContextFactory<GameStoreContext> _contextFactory;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(
        IDbContextFactory<GameStoreContext> contextFactory,
        IRepository<Game> gameRepository,
        IRepository<GamePlatform> gamePlatformRepository,
        IRepository<GameGenre> gameGenreRepository,
        IRepository<Genre> genreRepository,
        IRepository<Platform> platformRepository, 
        IRepository<Publisher> publishersRepository,
        IRepository<OrderGame> orderGameRepository,
        IRepository<Order> orderRepository,
        IRepository<PaymentMethod> paymentMethodRepository,
        IRepository<Comment> commentRepository,
        IRepository<BannedUser> bannedUsers,
        IUserRespository users,
        IRepository<Role> roles,
        IRepository<Permission> permissions,
        IRepository<UserRole> userRoles)
    {
        _contextFactory = contextFactory;
        Games = gameRepository;
        GameGenres = gameGenreRepository;
        GamePlatforms = gamePlatformRepository;
        Genres = genreRepository;
        Platforms = platformRepository;
        Publishers = publishersRepository;
        PaymentMethods = paymentMethodRepository;
        OrderGames = orderGameRepository;
        Orders = orderRepository;
        Comments = commentRepository;
        BannedUsers = bannedUsers;
        Users = users;
        Roles = roles;
        Permissions = permissions;
        UserRoles = userRoles;
    }

    public IRepository<Game> Games { get; }
    public IRepository<GameGenre> GameGenres { get; }
    public IRepository<Genre> Genres { get; }
    public IRepository<Platform> Platforms { get; }
    public IRepository<GamePlatform> GamePlatforms { get; }
    public IRepository<Publisher> Publishers { get; }
    public IRepository<OrderGame> OrderGames { get; }
    public IRepository<Order> Orders { get; }
    public IRepository<PaymentMethod> PaymentMethods { get; }
    public IRepository<Comment> Comments { get; }
    public IRepository<BannedUser> BannedUsers { get; }
    public IUserRespository Users { get; }
    public IRepository<Role> Roles { get; }
    public IRepository<Permission> Permissions { get; }
    public IRepository<UserRole> UserRoles { get; }

    public async Task CompleteAsync(CancellationToken ct = default)
    {
        using (var context = await _contextFactory.CreateDbContextAsync(ct))
        {
            await context.SaveChangesAsync(ct);
        }
    }
    
    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        if(_transaction is null)
        {
            return;
        }
    
        await _transaction.CommitAsync(ct);
        await _transaction.DisposeAsync();
        _transaction = null;

    }

    public async Task CommitTransactionAsync(CancellationToken ct = default)
    {
        if(_transaction == null)
        {
            return;
        }
        
        await _transaction.CommitAsync(ct);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken ct = default)
    {
        if(_transaction == null)
        {
            return;
        }
        
        await _transaction.RollbackAsync(ct);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public void Dispose()
    {
        using (var context = _contextFactory.CreateDbContext())
        {
            context.Dispose();
        }
    }
}