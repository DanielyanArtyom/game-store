using GameStore.Data.Common.Interface;

namespace GameStore.Data.Interface;

public interface IUnitOfWork: IBaseUnitOfWork
{
    IRepository<Game> Games { get; }
    IRepository<GameGenre> GameGenres { get; }
    IRepository<Genre> Genres { get; }
    IRepository<Platform> Platforms { get; }
    IRepository<GamePlatform> GamePlatforms { get; }
    IRepository<Publisher> Publishers { get; }
    IRepository<OrderGame> OrderGames { get; }
    IRepository<PaymentMethod> PaymentMethods { get; }
    IRepository<Order> Orders { get; }
    IRepository<Comment> Comments { get; }
    IRepository<BannedUser> BannedUsers { get; }
    IUserRespository Users { get; }
    IRepository<Role> Roles { get; }
    IRepository<Permission> Permissions { get; }
    IRepository<UserRole> UserRoles { get; }
    
    Task BeginTransactionAsync(CancellationToken ct = default);
    Task CommitTransactionAsync(CancellationToken ct = default);
    Task RollbackTransactionAsync(CancellationToken ct = default);

}