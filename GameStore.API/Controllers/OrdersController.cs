using GameStore.Business.Service;

namespace GameStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly BankPaymentService _bankPaymentService;
    private readonly IBoxPaymentService _iboxPaymentService;
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;
    private readonly VisaPaymentService _visaPaymentService;

    public OrdersController(
        IOrderService orderService,
        IPaymentService paymentService,
        BankPaymentService bankPaymentService,
        VisaPaymentService visaPaymentService,
        IBoxPaymentService iboxPaymentService,
        IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
        _paymentService = paymentService;
        _bankPaymentService = bankPaymentService;
        _visaPaymentService = visaPaymentService;
        _iboxPaymentService = iboxPaymentService;
    }

    [HttpDelete("/cart/{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveGameFromCart(string key)
    {
        var customerId = User.GetCustomerId();
        await _orderService.RemoveGameFromCartAsync(key, customerId);
        return Ok();
    }
    
    [HttpDelete("details/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleManager, 
        Policy = AuthorizationConstants.WritePermissionsPolicy)]
    public async Task<IActionResult> RemoveOrderDetails(Guid id, CancellationToken ct = default)
    {
        await _orderService.RemoveOrderDetail(id, ct);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllOrders()
    {
        var result = await _orderService.GetAllAsync();
        return Ok(_mapper.Map<List<OrderDto>>(result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(string id)
    {
        var customerId = User.GetCustomerId();
        var result = await _orderService.GetByIdAsync(id, customerId);
        return Ok(_mapper.Map<OrderDto>(result));
    }

    [HttpGet("{id}/details")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderGameDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderDetailsById(Guid id)
    {
        var customerId = User.GetCustomerId();
        var result = await _orderService.GetOrderDetailsAsync(id, customerId);
        return Ok(_mapper.Map<List<OrderGameDto>>(result));
    }

    [HttpGet("/cart")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderGameDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCart()
    {
        var customerId = User.GetCustomerId();
        var result = await _orderService.GetOpenCartDetailsAsync(customerId);
        return Ok(_mapper.Map<List<OrderGameDto>>(result));
    }
    
    [HttpPatch("details/{id}/quantity")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = "Manager", 
        Policy = "WritePermissionsPolicy")]
    public async Task<IActionResult> UpdateQuantity( Guid id, [FromBody] OrderUpdateRequest request, CancellationToken ct = default)
    {
        await _orderService.UpdateOrderQuantity(id, request.Count, ct);
        
        return Ok();
    }

    [HttpGet("/payment-methods")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentMethodDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPaymentMethods()
    {
        var result = await _paymentService.GetAllAsync();
        return Ok(_mapper.Map<List<PaymentMethodDto>>(result));
    }

    [HttpPost("/payment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var customerId = User.GetCustomerId();

        switch (request.Method)
        {
            case PaymentMethodEnum.Visa:
                await _visaPaymentService.ProcessPaymentAsync(request.Card!, customerId);
                return Ok();

            case PaymentMethodEnum.Bank:
                var bankResult = await _bankPaymentService.ProcessPaymentAsync(customerId);
                return File(bankResult!, "application/octet-stream", $"{customerId}-{DateTime.UtcNow}.pdf");

            case PaymentMethodEnum.IBoxTerminal:
                var iboxResult = await _iboxPaymentService.ProcessPaymentAsync(customerId);
                return Ok(iboxResult);

            default:
                return BadRequest($"Payment method {request.Method} is not supported.");
        }
    }
}