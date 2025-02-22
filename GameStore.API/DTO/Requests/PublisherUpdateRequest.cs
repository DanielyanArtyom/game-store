namespace GameStore.API.DTO.Requests;

public class PublisherUpdateRequest
{
    [Required] 
    public required Guid Id { get; set; }
    
    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Company Name must be between 3 and 100 characters.")]
    public string CompanyName { get; set; }
    
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Home Page must be between 8 and 100 characters.")]
    public string? HomePage { get; set; }
    
    [StringLength(100, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 100 characters.")]
    public string? Description { get; set; }
}