using GameStore.Data.Enum;

namespace GameStore.API.DTO.Responses;

public class PermissionDto: BaseDto
{
    public AccessTypesEnum AccessType { get; set; }
    public required ResourceEnum Resource { get; set; }
}