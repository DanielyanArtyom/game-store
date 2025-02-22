namespace GameStore.Data.Context.Entity;

public class BannedUser: BaseEntity
{
    public required Guid  UserId {get; set; }
    public required DateTime BannedFrom {get; set; }
    public required DateTime BannedTo {get; set; }
    
    public User User { get; set; }
}