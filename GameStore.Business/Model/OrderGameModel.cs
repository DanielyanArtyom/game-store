namespace GameStore.Business.Model;

public class OrderGameModel
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int? Discount { get; set; }
    
    public Order Order { get; set; }
}