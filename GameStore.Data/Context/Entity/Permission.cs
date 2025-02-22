using GameStore.Data.Enum;

namespace GameStore.Data.Context.Entity;

public class Permission: BaseEntity
{
    public AccessTypesEnum AccessType { get; set; }
    public required ResourceEnum Resource { get; set; }
    
    public required Guid RoleId { get; set; }
    public Role Role { get; set; }
    
}