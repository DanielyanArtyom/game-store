namespace GameStore.Data.Context.Entity;

public class OrderGame: BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    
    public Game Game { get; set; }
    public Order Order { get; set; }
}