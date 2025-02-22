namespace GameStore.API.DTO.Requests;

public class LoginRequest
{
    [Required]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Login must be between 5 and 15 characters.")]
    public required string Login { get; set; }
    
    [Required]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 15 characters.")]
    public required string Password { get; set; }
}