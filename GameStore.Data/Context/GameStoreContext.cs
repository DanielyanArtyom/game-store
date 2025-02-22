namespace GameStore.Data.Context;

public class GameStoreContext : DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games { get; set; } = default!;
    public DbSet<Genre> Genres { get; set; } = default!;
    public DbSet<Platform> Platforms { get; set; } = default!;
    public DbSet<GameGenre> GameGenres { get; set; } = default!;
    public DbSet<GamePlatform> GamePlatforms { get; set; } = default!;
    public DbSet<Publisher> Publishers { get; set; } = default!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderGame> OrderGames { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;
    public DbSet<BannedUser> BannedUsers { get; set; } = default!;
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameStoreContext).Assembly);
    }
}