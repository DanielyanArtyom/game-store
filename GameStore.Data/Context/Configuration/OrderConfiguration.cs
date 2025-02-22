namespace GameStore.Data.Configuration;

public class OrderConfiguration: IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasMany(o => o.OrderGames)
            .WithOne(og => og.Order)
            .HasForeignKey(og => og.OrderId);

        builder.Ignore(o => o.TotalSum);
    }
}