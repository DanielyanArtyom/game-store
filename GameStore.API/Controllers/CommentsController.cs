namespace GameStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICommentService _commentService;
    private readonly IBannedUserService _bannedUserService;

    public CommentsController(ICommentService commentService, IBannedUserService bannedUserService ,IMapper mapper)
    {
        _mapper = mapper;
        _commentService = commentService;
        _bannedUserService = bannedUserService;
    }
    
    [HttpGet("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommentDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetComments(string key)
    {
        var comments = await _commentService.GetCommentsAsync(key);
        return Ok(_mapper.Map<List<CommentDto>>(comments));
    }
     
    [HttpPost("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleUser, 
        Policy = AuthorizationConstants.WritePermissionsPolicy)]
    public async Task<IActionResult> CreateComment(string key, [FromBody] AddCommentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = User.GetCustomerId();
        var userName = User.GetName();
        
        var req = _mapper.Map<CommentModel>(request);

        req.UserId = userId;
        req.Name = userName;
        
        await _commentService.CreateCommentAsync(req, key);
        return Ok();
    }
    
    [HttpPost("ban")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleModerator, 
        Policy = AuthorizationConstants.WritePermissionsPolicy)]
    public async Task<IActionResult> BanUser([FromBody] UserBanRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var req = _mapper.Map<UserBanModel>(request);
        await _bannedUserService.BanUserAsync(req);
        return Ok();
    }
    
    [HttpDelete("{key}/{commentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteComment(string key, Guid commentId)
    {
        await _commentService.RemoveCommentAsync(key, commentId);
        return Ok();
    }
}