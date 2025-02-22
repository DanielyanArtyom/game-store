using System.Security.Claims;

namespace GameStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly IGenreService _genreService;
    private readonly IPlatformService _platformService;
    private readonly IPublisherService _publisherService;
    private readonly IOrderService _orderService;
    
    private readonly IMapper _mapper;
    
    public GamesController(
        IGameService gameService,
        IGenreService genreService, 
        IPlatformService platformService, 
        IPublisherService publisherService, 
        IOrderService orderService ,
        IMapper mapper)
    {
        _genreService = genreService;
        _gameService = gameService;
        _platformService = platformService;
        _publisherService = publisherService;
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleManager, 
        Policy = AuthorizationConstants.WritePermissionsPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateGame([FromBody] GameCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var req = _mapper.Map<GameModel>(request);
        await _gameService.CreateAsync(req);
        return Ok();
    }
    
    [HttpPost("{key}/buy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddGameToCart(string key)
    {
        var customerId = User.GetCustomerId();
        await _orderService.AddGameToCartAsync(key, customerId);
        return Ok();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleManager, 
        Policy = AuthorizationConstants.WritePermissionsPolicy)]
    public async Task<IActionResult> UpdateGame([FromBody] GameUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var req = _mapper.Map<GameModel>(request);
        await _gameService.UpdateAsync(req);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GameDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllGamesByFilter([FromQuery] GameSearchFilters filters, CancellationToken ct = default)
    {
        var games = await _gameService.SearchAsync(_mapper.Map<GameSearchFilterModel>(filters), ct);
        
        Response.SetNoCacheHeaders();
        
        return Ok(_mapper.Map<PagedGamesDto>(games));
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GameDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllGames()
    {
        var games = await _gameService.GetAllAsync();
        
        Response.SetNoCacheHeaders();
        
        return Ok(_mapper.Map<PagedGamesDto>(games));
    }

    [HttpGet("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByKey(string key)
    {
        var gameTask =  _gameService.GetByKeyAsync(key);
        var incrementTask = _gameService.IncrementViewCountAsync(key);

        await Task.WhenAll(gameTask, incrementTask);
        
        Response.SetNoCacheHeaders();
        
        return Ok(_mapper.Map<GameDto>(gameTask.Result));
    }

    [HttpGet("find/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var game = _gameService.GetByIdAsync(id);

        await Task.WhenAll(game, _gameService.IncrementViewCountByIdAsync(id));
       
        return Ok(_mapper.Map<GameDto>(game.Result));
    }
    
    [HttpGet("{key}/genres")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGenresByKey(string key)
    {
        var genres = await _genreService.GetByGameKeyAsync(key);
        return Ok(_mapper.Map<List<GenreDto>>(genres));
    }

    [HttpGet("{key}/platforms")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlatformDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlatformsByKey(string key)
    {
        var platforms = await _platformService.GetByGameKeyAsync(key);
        return Ok(_mapper.Map<List<PlatformDto>>(platforms));
    }
    
    [HttpGet("{key}/publishers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublisherDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPublishersByKey(string key)
    {
        var publisher = await _publisherService.GetByGameKeyAsync(key);
        return Ok(_mapper.Map<PublisherDto>(publisher));
    }

    [HttpDelete("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleManager, 
        Policy = AuthorizationConstants.WritePermissionsPolicy)]
    public async Task<IActionResult> DeleteByKey(string key)
    {
        await _gameService.DeleteGameAsync(key);
        return Ok();
    }

    [HttpGet("{key}/file")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadFile(string key)
    {
        var fileBytes = await _gameService.DownladGameFileAsync(key);
        return File(fileBytes, "application/octet-stream", $"{key}.txt");
    }
}