namespace GameStore.Business.Model;

public class OrderModel : BaseModel
{
    public required DateTime? Date { get; set; }
    public required Guid CustomerId { get; set; }
    public required OrderStatus Status { get; set; }
    public int? OriginalId { get; set; }
    
    public List<OrderGame> OrderGames { get; set; }
}