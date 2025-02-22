using GameStore.Data.Enum;

namespace GameStore.API.DTO.Requests;

public class CheckAccessRequest
{
    [Required]
    public required string TargetPage { get; set; }
    
    [Required]
    public required AccessTypesEnum Permission { get; set; }
}