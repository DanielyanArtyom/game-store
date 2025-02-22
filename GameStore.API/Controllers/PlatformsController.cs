namespace GameStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly IMapper _mapper;
    private readonly IPlatformService _platformService;

    public PlatformsController(IGameService gameService, IPlatformService platformService, IMapper mapper)
    {
        _gameService = gameService;
        _mapper = mapper;
        _platformService = platformService;
    }

    [HttpGet("{id}/games")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GameDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGamesById(Guid id)
    {
        var games = await _gameService.GetGamesByPlatformIdAsync(id);
        return Ok(_mapper.Map<List<GameDto>>(games));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleManager, 
        Policy = AuthorizationConstants.WritePermissionsPolicy)]
    public async Task<IActionResult> CreatePlatform([FromBody] PlatformCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _platformService.CreateAsync(_mapper.Map<PlatformModel>(request));
        return Ok();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleManager, 
        Policy = AuthorizationConstants.WritePermissionsPolicy)]
    public async Task<IActionResult> UpdatePlatform([FromBody] PlatformUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _platformService.UpdateAsync(_mapper.Map<PlatformModel>(request));
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var platforms = await _platformService.GetAllAsync();
        return Ok(_mapper.Map<List<PlatformModel>>(platforms));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var platform = await _platformService.GetByIdAsync(id);
        return Ok(_mapper.Map<PlatformModel>(platform));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleManager, 
        Policy = AuthorizationConstants.WritePermissionsPolicy)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _platformService.DeleteAsync(id);
        return Ok();
    }
}