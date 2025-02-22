namespace GameStore.API.DTO.Responses;

public class CommentDto: BaseDto
{
    public required Guid UserId { get; set; }
    public required string Body { get; set; }
    public required string Name { get; set; }
    public Guid? ParentCommentId { get; set; }
    
    public List<CommentDto> ChildComments { get; set; } = new();
}