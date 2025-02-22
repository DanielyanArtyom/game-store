namespace GameStore.API.DTO.Requests;

public class GenreUpdateRequest
{
    [Required] 
    public required Guid Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 20 characters.")]
    public required string Name { get; set; }

    public Guid? ParentGenreId { get; set; }
}