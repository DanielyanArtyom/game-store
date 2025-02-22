namespace GameStore.Business.Model;

public class UserModel: BaseModel
{
    public required string Name { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public List<Role> Roles { get; set; }
    
    public List<BannedUser> BannedUsers { get; set; }
}