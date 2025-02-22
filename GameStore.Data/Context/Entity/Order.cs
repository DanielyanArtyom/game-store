using GameStore.Data.Enum;

namespace GameStore.Data.Context.Entity;

public class Order : BaseEntity
{
    public required DateTime? Date { get; set; }
    public required Guid CustomerId { get; set; }
    public required OrderStatus Status { get; set; }
    public int? OriginalId { get; set; }

    public List<OrderGame> OrderGames { get; set; } = new();
    public decimal TotalSum => OrderGames.Sum(el => (el.Price - el.Discount) * el.Quantity);
}