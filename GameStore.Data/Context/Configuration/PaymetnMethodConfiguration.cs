namespace GameStore.Data.Configuration;

public class PaymetnMethodConfiguration: IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.HasData(
            new PaymentMethod
            {
                Id = new Guid("9f2592f7-9768-4c2e-933f-50cf14b1f896"), 
                Title = "Bank", 
                Description = "Allow customers to pay directly from their bank account to another bank account",
                ImageUrl = "https://cdn5.vectorstock.com/i/1000x1000/41/09/payment-terminal-icon-simple-style-vector-22764109.jpg"
            },
            new PaymentMethod
            { 
                Id = new Guid("94a5703d-6ba8-42e5-82d7-7fcfcb050ab4"), 
                Title = "IBox terminal", 
                Description = "The ibox system is built to provide maximum protection for client and user data", 
                ImageUrl = "https://a0.anyrgb.com/pngimg/990/1580/ibox-new-payment-terminal-privatbank-rates-chernihiv-selfservice-ukraine-value-cash.png"
                
            },
            new PaymentMethod
            {
                Id = new Guid("5cdfec58-07db-499c-85ae-3ebcbafb5b5d"), 
                Title = "Visa",
                Description = "Visa is a global payment technology business that connects customers, businesses, banks, and governments, enabling them to use digital currency instead of cash or checks",
                ImageUrl = "https://www.visa.com.au/dam/VCOM/regional/ve/romania/blogs/hero-image/visa-logo-800x450.jpg"
            }
        );
    }
}