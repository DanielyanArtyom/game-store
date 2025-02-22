namespace GameStore.Data.Configuration;

public class UserRoleConfiguration: IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder
            .HasKey(ur => new { ur.UserId, ur.RoleId });
        
        builder
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);
        
        builder
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
        
        builder.HasData(
            new UserRole
            {
                Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c5050"),
                UserId = new Guid("d27fc764-f53d-40cb-a6f9-629f433c4040"), 
                RoleId = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030"),
            }
        );
    }
}