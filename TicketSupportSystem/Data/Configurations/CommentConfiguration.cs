using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSupportSystem.Data.Entities;

namespace TicketSupportSystem.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(comment => comment.Text)
                    .IsRequired();
            builder.Property(comment => comment.CreatedAt)
                    .IsRequired();
            builder.HasMany(comment => comment.Attachments)
                 .WithOne(attach => attach.Comment)
                 .HasForeignKey(attach => attach.CommentId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
