namespace GameStore.Data.Configuration;

public class GamePlatformConfiguration : IEntityTypeConfiguration<GamePlatform>
{
    public void Configure(EntityTypeBuilder<GamePlatform> builder)
    {
        builder
            .HasKey(gp => new { gp.GameId, gp.PlatformId });

        builder
            .HasIndex(gp => new { gp.GameId, gp.PlatformId })
            .IsUnique();
    }
}