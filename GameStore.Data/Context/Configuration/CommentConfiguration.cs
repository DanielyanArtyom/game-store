namespace GameStore.Data.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .HasMany(c => c.ChildComments)
            .WithOne(c => c.ParentComment)
            .HasForeignKey(c => c.ParentCommentId);

        builder
            .HasOne(c => c.Game)
            .WithMany(g => g.Comments)
            .HasForeignKey(c => c.GameId);
    }
}