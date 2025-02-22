namespace GameStore.Data.Configuration;

public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
{
    public void Configure(EntityTypeBuilder<GameGenre> builder)
    {
        builder
            .HasKey(gg => new { gg.GameId, gg.GenreId });

        builder
            .HasIndex(gg => new { gg.GameId, gg.GenreId })
            .IsUnique();
    }
}