namespace GameStore.Data.Configuration;

public class BannedUserConfiguration: IEntityTypeConfiguration<BannedUser>
{
    public void Configure(EntityTypeBuilder<BannedUser> builder)
    {
        builder
            .HasOne(b => b.User)
            .WithMany(u => u.BannedUsers)
            .HasForeignKey(b => b.UserId);
    }
}