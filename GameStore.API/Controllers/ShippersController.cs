namespace GameStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShippersController: ControllerBase
{
    private readonly IShipperService _shipperService;
    private readonly IMapper _mapper;
    
    public ShippersController(IShipperService shipperService, IMapper mapper)
    {
        _shipperService = shipperService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ShipperDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSuppliers()
    {
        var shippers = await _shipperService.GetShippers();
        
        return Ok(_mapper.Map<List<ShipperDto>>(shippers));
    }
}