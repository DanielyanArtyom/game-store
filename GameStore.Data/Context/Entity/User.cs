namespace GameStore.Data.Context.Entity;

public class User: BaseEntity
{
    public required string Name { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }

    public List<BannedUser> BannedUsers { get; set; } = new();
    public List<UserRole> UserRoles { get; set; } = new();
}