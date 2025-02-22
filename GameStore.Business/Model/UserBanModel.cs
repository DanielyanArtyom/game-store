namespace GameStore.Business.Model;

public class UserBanModel
{
    public required Guid UserId { get; set; }
    public required BanDuration Duration { get; set; }
}