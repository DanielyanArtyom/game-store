namespace GameStore.API.DTO.Requests;

public class GameUpdateRequest
{
    [Required] 
    public required Guid Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
    public required string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Key must be between 3 and 100 characters.")]
    public required string Key { get; set; } = string.Empty;
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public required decimal Price { get; set; }
    
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Unit of stocks must be positive number")]
    public required int UnitInStock { get; set; }
    
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Unit of stocks must be positive number")]
    public required decimal Discount { get; set; }
    
    [Required]
    public Guid PublisherId { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string? Description { get; set; }
    
    [Required]
    public int ReorderLevel { get; set; }

    [Required] 
    public bool Discontinued { get; set; } = false;
    
    [Required]
    public string QuantityPerUnit { get; set; }
    
    [Required]
    public int UnitsOnOrder { get; set; }

    [MinLength(1)]
    public List<Guid> Genres { get; set; } = new();

    [MinLength(1)] 
    public List<Guid> Platforms { get; set; } = new();
}