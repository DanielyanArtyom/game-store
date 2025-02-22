namespace GameStore.Data.Configuration;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder
            .HasIndex(g => g.Key)
            .IsUnique();

        builder
            .HasOne(g => g.Publisher)
            .WithMany(p => p.Games)
            .HasForeignKey(g => g.PublisherId);
        
        builder
            .HasMany(g => g.GameGenres)
            .WithOne(gg => gg.Game)
            .HasForeignKey(gg => gg.GameId);

        builder.HasMany(g => g.GamePlatforms)
            .WithOne(gp => gp.Game)
            .HasForeignKey(gp => gp.GameId);
    }
}