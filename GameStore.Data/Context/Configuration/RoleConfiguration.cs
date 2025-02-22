namespace GameStore.Data.Configuration;

public class RoleConfiguration: IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasIndex(u => u.Name)
            .IsUnique();
        
        builder.HasData(
            new Role { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030"), Name = "Administrator" },
            new Role { Id = new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5"), Name = "Manager" },
            new Role { Id = new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0"), Name = "Moderator" },
            new Role { Id = new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce"), Name = "User" },
            new Role { Id = new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee"), Name = "Guest" }
        );
    }
}