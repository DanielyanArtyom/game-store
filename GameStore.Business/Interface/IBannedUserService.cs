namespace GameStore.Business.Interface;

public interface IBannedUserService
{
    Task BanUserAsync(UserBanModel request, CancellationToken ct = default);
}