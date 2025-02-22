using GameStore.Business.Enum;

namespace GameStore.Business.Service;

public class CommentsService: ICommentService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVisitor _visitor;
    
    public CommentsService(IUnitOfWork unitOfWork, IVisitor visitor, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _visitor = visitor;
    }
    
    public async Task CreateCommentAsync(CommentModel request, string gameKey, CancellationToken ct = default)
    {
        await IsBannedUser(request.UserId, ct);
        _visitor.Visit(request);

        var game = (await _unitOfWork.Games.SearchAsync(new SearchContext<Game> { Filter = x => x.Key == gameKey }, ct)).Results.FirstOrDefault();

        if (game == null)
        {
            throw new NotFoundException("Game not found");
        }

        if (request.Action != null)
        {
            var parentComment = (await _unitOfWork.Comments.SearchAsync(new SearchContext<Comment>
                { Filter = x => x.Id == request.ParentCommentId }, ct)).Results.FirstOrDefault();

            if (parentComment == null)
            {
                throw new NotFoundException("Parent comment not found");
            }

            request.Body = request.Action == ActionType.Reply
                ? $"[{parentComment.Name}], {request.Body}"
                : $"[{parentComment.Body}], {request.Body}";
        }
        
        var comment = _mapper.Map<Comment>(request);
        comment.GameKey = gameKey;
        comment.GameId = game.Id;
        
        _unitOfWork.Comments.Add(comment);
        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task<List<CommentModel>> GetCommentsAsync(string gameKey)
    {
        var comment = await _unitOfWork.Comments.SearchAsync(new SearchContext<Comment>
        {
            Filter = x => x.GameKey == gameKey && x.ParentCommentId == null, 
            Include = new Expression<Func<Comment, object>>[] { x => x.ChildComments }
        });
        return _mapper.Map<List<CommentModel>>(comment);
    }

    public async Task RemoveCommentAsync(string gameKey, Guid commentId, CancellationToken ct = default)
    {
        var comment = (await _unitOfWork.Comments.SearchAsync(new SearchContext<Comment>
        {
            Filter = x => x.GameKey == gameKey && x.Id == commentId, 
            Include = new Expression<Func<Comment, object>>[] { x => x.ChildComments }
        },ct)).Results.FirstOrDefault();

        if (comment == null)
        {
            throw new NotFoundException("Comment is not found");
        }

        comment.Body = "A comment/quote was deleted";
        
        _unitOfWork.Comments.Update(commentId, comment);
        
        comment.ChildComments.ForEach(el =>
        {
            el.Body = "A comment/quote was deleted";
            _unitOfWork.Comments.Update(el.Id, el);
        });

        await _unitOfWork.CompleteAsync(ct);
    }

    #region  PrivateMethods
    
    private async Task IsBannedUser(Guid userId, CancellationToken ct = default)
    {
        var user = await _unitOfWork.BannedUsers.GetByIdAsync(userId, ct);

        if (user != null)
        {
            throw new ForbiddenException($"User banned until {user.BannedTo}");
        }
    }

    #endregion
}