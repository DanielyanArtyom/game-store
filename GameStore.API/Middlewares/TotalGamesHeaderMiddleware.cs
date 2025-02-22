using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace GameStore.API.Middlewares;

public class TotalGamesHeaderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMemoryCache _cache;
    private readonly AppSettingOptions  _options;

    public TotalGamesHeaderMiddleware(RequestDelegate next, IMemoryCache cache, IOptions<AppSettingOptions> options)
    {
        _next = next;
        _cache = cache;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var gameService = context.RequestServices.GetRequiredService<IGameService>();
        
        var totalGames = await GetTotalGamesAsync(gameService);
        
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Append("x-total-numbers-of-games", totalGames.ToString());
            return Task.CompletedTask;
        });

        await _next(context);
    }

    private async Task<int> GetTotalGamesAsync(IGameService gameService)
    {
        if (!_cache.TryGetValue("TotalGames", out int totalGames))
        {
            totalGames = await gameService.GetGamesCountAsync();
            _cache.Set("TotalGames", totalGames, TimeSpan.FromMinutes(_options.CachingTimeByMinutes));
        }

        return totalGames;
    }
}