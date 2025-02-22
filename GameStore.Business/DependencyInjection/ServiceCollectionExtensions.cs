namespace GameStore.Business.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGameStoreServices(this IServiceCollection services, PaymentServiceOptions paymentServiceOptions)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton(paymentServiceOptions);
        services.AddHttpClient<IPaymentClient, PaymentClient>();
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<IVisitor, ValidationVisitor>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IPlatformService, PlatformService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICommentService, CommentsService>();
        services.AddScoped<IBannedUserService, BannedUserService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPredefinedDataSetsProvider, PredefinedDataSetsProvider>();
        services.AddScoped<ISyncService, SyncService>();
        services.AddScoped<IGamesManager, GamesManager>();
        services.AddScoped<IOrdersManager, OrdersManager>();
        services.AddScoped<ITransactionsManager, TransactionsManager>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IShipperService, ShipperService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<VisaPaymentService>();
        services.AddScoped<IBoxPaymentService>();
        services.AddScoped<BankPaymentService>();
        return services;
    }
}