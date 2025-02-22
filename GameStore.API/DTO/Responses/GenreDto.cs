namespace GameStore.API.DTO.Responses;

public class GenreDto : BaseDto
{ 
    public required string Name { get; set; }
    public Guid? ParentGenreId { get; set; }
}