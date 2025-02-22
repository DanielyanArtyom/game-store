namespace GameStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublishersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPublisherService _publisherService;
    private readonly IGameService _gameService;
    
    public PublishersController(IPublisherService publisherService, IGameService gameService ,IMapper mapper)
    {
        _mapper = mapper;
        _publisherService = publisherService;
        _gameService = gameService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleManager, 
        Policy = AuthorizationConstants.WritePermissionsPolicy)]
    public async Task<IActionResult> CreatePublisher([FromBody] PublisherCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var req = _mapper.Map<PublisherModel>(request);
        await _publisherService.CreateAsync(req);
        return Ok();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleManager, 
        Policy = AuthorizationConstants.WritePermissionsPolicy)]
    public async Task<IActionResult> UpdatePublisher([FromBody] PublisherUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var req = _mapper.Map<PublisherModel>(request);
        await _publisherService.UpdateAsync(req);
        return Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PublisherDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var publishers = await _publisherService.GetAllAsync();
        return Ok(_mapper.Map<List<PublisherDto>>(publishers));
    }
    
    [HttpGet("{id}/find")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublisherDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var publisher = await _publisherService.GetByIdAsync(id);
        return Ok(_mapper.Map<PublisherDto>(publisher));
    }
    
    [HttpGet("{companyName}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublisherDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCompanyName(string companyName)
    {
        var publisher = await _publisherService.GetByCompanyNameAsync(companyName);
        return Ok(_mapper.Map<PublisherDto>(publisher));
    }
    
    [HttpGet("{companyName}/games")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GameDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGamesByCompany(string companyName)
    {
        var publisher = await _gameService.GetGamesByCompanyNameAsync(companyName);
        return Ok(_mapper.Map<List<GameDto>>(publisher));
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
        await _publisherService.DeleteAsync(id);
        return Ok();
    }
}