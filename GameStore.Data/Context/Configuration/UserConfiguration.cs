namespace GameStore.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(u => u.Login)
            .IsUnique();

        builder.HasData(
            new User { 
                Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c4040"), 
                Name = "Administrator", 
                Login = "admin", 
                Password = BCrypt.Net.BCrypt.HashPassword("admin") 
            }
        );
    }
}