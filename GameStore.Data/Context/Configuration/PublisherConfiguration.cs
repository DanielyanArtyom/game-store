namespace GameStore.Data.Configuration;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder
            .HasIndex(p => p.CompanyName)
            .IsUnique();
    }
}