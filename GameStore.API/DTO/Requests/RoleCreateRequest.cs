namespace GameStore.API.DTO.Requests;

public class RoleCreateRequest
{
    [Required] 
    public string Name { get; set; }
    
    [Required] 
    public PermissionDto Permissions { get; set; }
}