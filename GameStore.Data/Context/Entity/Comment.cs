namespace GameStore.Data.Context.Entity;

public class Comment: BaseEntity
{
    public Guid? ParentCommentId { get; set; }
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
    public required string GameKey { get; set; }
    public required string Name { get; set; }
    public required string Body { get; set; }
    
    public List<Comment> ChildComments { get; set; } = new();
    public Comment? ParentComment { get; set; }
    public Game? Game { get; set; }
}