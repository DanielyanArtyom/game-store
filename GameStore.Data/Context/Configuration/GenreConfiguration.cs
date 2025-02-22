namespace GameStore.Data.Configuration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        var strategyId = new Guid("01e73b48-bf3e-45df-a107-c65f302ce8db");
        var racesId = new Guid("4ebd532a-2c0f-4b3a-96c3-06d77f7a432d");
        var actionId = new Guid("8ba799e7-6a4a-4302-b6cc-e5ab871cd8aa");

        builder
            .HasIndex(g => g.Name)
            .IsUnique();

        builder
            .HasMany(g => g.SubGenres)
            .WithOne(g => g.ParentGenre)
            .HasForeignKey(g => g.ParentGenreId);

        builder
            .HasMany(g => g.GameGenres)
            .WithOne(g => g.Genre)
            .HasForeignKey(g => g.GenreId);

        builder.HasData(
            new Genre { Id = strategyId, Name = "Strategy" },
            new Genre { Id = new Guid("02e73b48-bf3e-45df-a107-c65f302ce8dc"), Name = "RTS", ParentGenreId = strategyId },
            new Genre {Id = new Guid("03e73b48-bf3e-45df-a107-c65f302ce8dd"), Name = "TBS", ParentGenreId = strategyId },
            new Genre { Id = new Guid("04e73b48-bf3e-45df-a107-c65f302ce8de"), Name = "RPG" },
            new Genre { Id = new Guid("05e73b48-bf3e-45df-a107-c65f302ce8df"), Name = "Sports" },
            new Genre { Id = racesId, Name = "Races" },
            new Genre { Id = new Guid("07e73b48-bf3e-45df-a107-c65f302ce8e1"), Name = "Rally", ParentGenreId = racesId },
            new Genre { Id = new Guid("08e73b48-bf3e-45df-a107-c65f302ce8e2"), Name = "Arcade", ParentGenreId = racesId },
            new Genre { Id = new Guid("09e73b48-bf3e-45df-a107-c65f302ce8e3"), Name = "Formula", ParentGenreId = racesId },
            new Genre { Id = new Guid("10e73b48-bf3e-45df-a107-c65f302ce8e4"), Name = "Off-road", ParentGenreId = racesId },
            new Genre { Id = actionId, Name = "Action" },
            new Genre { Id = new Guid("12e73b48-bf3e-45df-a107-c65f302ce8e6"), Name = "FPS", ParentGenreId = actionId },
            new Genre { Id = new Guid("13e73b48-bf3e-45df-a107-c65f302ce8e7"), Name = "TPS", ParentGenreId = actionId },
            new Genre { Id = new Guid("14e73b48-bf3e-45df-a107-c65f302ce8e8"), Name = "Adventure" },
            new Genre { Id = new Guid("15e73b48-bf3e-45df-a107-c65f302ce8e9"), Name = "Puzzle & Skill" }
        );
    }
}