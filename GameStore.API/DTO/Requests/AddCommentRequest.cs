namespace GameStore.API.DTO.Requests;

public class AddCommentRequest
{
    [Required]
    public required string Body { get; set; }
    
    public Guid? ParentCommentId { get; set; }
    public ActionType? Action { get; set; }
}