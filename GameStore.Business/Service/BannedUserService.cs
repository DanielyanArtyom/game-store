namespace GameStore.Business.Service;

public class BannedUserService: IBannedUserService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public BannedUserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task BanUserAsync(UserBanModel request, CancellationToken ct = default)
    {
        var user = await _unitOfWork.BannedUsers.GetByIdAsync(request.UserId, ct);

        if (user != null)
        {
            throw new ArgumentException("User already banned");
        }

        DateTime banStart = DateTime.UtcNow;
        DateTime banEnd = GetBannedEnd(request.Duration);

        // TODO add relations with users, instead of hardcoding name
        var bannedUser = new BannedUser
        {
            UserId = request.UserId,
            BannedTo = banEnd,
            BannedFrom = banStart,
        };
        
        _unitOfWork.BannedUsers.Add(bannedUser);
        await _unitOfWork.CompleteAsync(ct);
    }

    private DateTime GetBannedEnd(BanDuration bannedDuration)
    {
        switch (bannedDuration)
        {
            case BanDuration.OneHour:
                return DateTime.UtcNow.AddHours(1);
            case BanDuration.OneDay:
                return DateTime.UtcNow.AddDays(1);
            case BanDuration.OneWeek:
                return DateTime.UtcNow.AddDays(7);
            case BanDuration.OneMonth:
                return DateTime.UtcNow.AddMonths(1);
            case BanDuration.Permanent:
            default:
                return DateTime.MaxValue;
        }
    }
}