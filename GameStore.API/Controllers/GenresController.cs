namespace GameStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;

    public GenresController(IGenreService genreService, IGameService gameService, IMapper mapper)
    {
        _genreService = genreService;
        _gameService = gameService;
        _mapper = mapper;
    }

    [HttpGet("{id}/games")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GameDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGamesById(Guid id)
    {
        var games = await _gameService.GetGamesByGenreIdAsync(id);
        return Ok(_mapper.Map<List<GameDto>>(games));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = "Manager", 
        Policy = "WritePermissionsPolicy")]
    public async Task<IActionResult> CreateGenre([FromBody] GenreCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _genreService.CreateAsync(_mapper.Map<GenreModel>(request));
        return Ok();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleManager, 
        Policy =AuthorizationConstants.WritePermissionsPolicy)]
    public async Task<IActionResult> UpdateGenre([FromBody] GenreUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _genreService.UpdateAsync(_mapper.Map<GenreModel>(request));
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var genres = await _genreService.GetAllAsync();
        return Ok(_mapper.Map<List<GenreModel>>(genres));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var genre = await _genreService.GetByIdAsync(id);
        return Ok(genre);
    }

    [HttpGet("{parentId}/genres")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByParenId(Guid parentId)
    {
        var genre = await _genreService.GetByParentIdAsync(parentId);
        return Ok(genre);
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
        await _genreService.DeleteAsync(id);
        return Ok();
    }
}