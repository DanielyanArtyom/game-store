namespace GameStore.Data.Configuration;

public class OrderGameConfiguration: IEntityTypeConfiguration<OrderGame>
{ 
    public void Configure(EntityTypeBuilder<OrderGame> builder)
    {
        builder
            .HasKey(gp => new { gp.OrderId, gp.ProductId });

        builder
            .HasIndex(gp => new { gp.OrderId, gp.ProductId })
            .IsUnique();
        
        builder.HasOne<Game>(gp => gp.Game).WithMany(g => g.OrderGames).HasForeignKey(x => x.ProductId);
    }
}