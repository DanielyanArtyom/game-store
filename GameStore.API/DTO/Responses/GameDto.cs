namespace GameStore.API.DTO.Responses;

public class GameDto : BaseDto
{
    public required string Name { get; set; }
    public required string Key { get; set; }
    public required decimal Price { get; set; }
    public required int UnitInStock { get; set; }
    public required decimal Discount { get; set; }
    public string? Description { get; set; }
    public required DateTime PublishDate { get; set; }
    public required int Views { get; set; }
    public int? OriginalId { get; set; }
    public required int ReorderLevel { get; set; }
    public required bool Discontinued { get; set; }
    public required string QuantityPerUnit { get; set; }
    public required int UnitsOnOrder { get; set; }
}