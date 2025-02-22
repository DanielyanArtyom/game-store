namespace GameStore.Business.Model;

public class CommentModel: BaseModel
{
    public required Guid UserId { get; set; }
    public required string Body { get; set; }
    public required string Name { get; set; }
    
    public Guid? ParentCommentId { get; set; }
    public ActionType? Action { get; set; }

    public List<CommentModel> ChildComments { get; set; } = new();
}