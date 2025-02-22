namespace GameStore.Data.Configuration;

public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder
            .HasIndex(p => p.Type)
            .IsUnique();

        builder
            .HasMany(p => p.GamePlatforms)
            .WithOne(p => p.Platform)
            .HasForeignKey(p => p.PlatformId);

        builder.HasData(
            new Platform { Id = new Guid("01f73b48-bf3e-45df-a107-c65f302ce8fa"), Type = "Mobile" },
            new Platform { Id = new Guid("02f73b48-bf3e-45df-a107-c65f302ce8fb"), Type = "Browser" },
            new Platform { Id = new Guid("03f73b48-bf3e-45df-a107-c65f302ce8fc"), Type = "Desktop" },
            new Platform { Id = new Guid("04f73b48-bf3e-45df-a107-c65f302ce8fd"), Type = "Console" }
        );
    }
}