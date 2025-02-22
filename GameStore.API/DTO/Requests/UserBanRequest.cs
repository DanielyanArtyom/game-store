namespace GameStore.API.DTO.Requests;

public class UserBanRequest
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public BanDuration Duration { get; set; }
}