using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSupportSystem.Data.Entities;

namespace TicketSupportSystem.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(ticket => ticket.Title)
                    .HasMaxLength(100)
                    .IsRequired();
            builder.Property(ticket => ticket.Description)
                    .IsRequired();
            builder.Property(ticket => ticket.Priority)
                    .IsRequired();
            builder.Property(ticket => ticket.Status)
                    .IsRequired();
            builder.Property(ticket => ticket.CreatedAt)
                    .IsRequired();
            builder.HasMany(user => user.Comments)
                 .WithOne(comment => comment.Ticket)
                 .HasForeignKey(comment => comment.TicketId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
