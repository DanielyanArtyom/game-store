namespace GameStore.Data.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGameStoreProvider(this IServiceCollection services, string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        services.AddPooledDbContextFactory<GameStoreContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IRepository<Game>, BaseRepository<Game>>();
        services.AddScoped<IRepository<GameGenre>, BaseRepository<GameGenre>>();
        services.AddScoped<IRepository<GamePlatform>, BaseRepository<GamePlatform>>();
        services.AddScoped<IRepository<Genre>, BaseRepository<Genre>>();
        services.AddScoped<IRepository<Platform>, BaseRepository<Platform>>();
        services.AddScoped<IRepository<Publisher>, BaseRepository<Publisher>>();
        services.AddScoped<IRepository<OrderGame>, BaseRepository<OrderGame>>();
        services.AddScoped<IRepository<Order>, BaseRepository<Order>>();
        services.AddScoped<IRepository<Comment>, BaseRepository<Comment>>();
        services.AddScoped<IRepository<BannedUser>, BaseRepository<BannedUser>>();
       // services.AddScoped<IRepository<User>, BaseRepository<User>>();
        services.AddScoped<IRepository<Role>, BaseRepository<Role>>();
        services.AddScoped<IRepository<Permission>, BaseRepository<Permission>>();
        services.AddScoped<IRepository<UserRole>, BaseRepository<UserRole>>();
        services.AddScoped<IRepository<PaymentMethod>, BaseRepository<PaymentMethod>>();
        services.AddScoped<IUserRespository, UserRespository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRevertRepository, RevertRepository>();
        
        return services;
    }
}