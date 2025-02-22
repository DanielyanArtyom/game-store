namespace GameStore.Business.Interface;

public interface ICommentService
{
    Task CreateCommentAsync(CommentModel request, string gameKey, CancellationToken ct = default);
    Task<List<CommentModel>> GetCommentsAsync(string gameKey);
    Task RemoveCommentAsync(string gameKey, Guid commentId, CancellationToken ct = default);
}