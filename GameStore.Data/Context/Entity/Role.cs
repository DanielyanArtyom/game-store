namespace GameStore.Data.Context.Entity;

public class Role: BaseEntity
{
    public required string Name { get; set; }
    
    public List<UserRole> UserRoles { get; set; } = new();
    public List<Permission> Permissions { get; set; } = new();
}