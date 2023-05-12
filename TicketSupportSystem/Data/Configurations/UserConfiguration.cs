using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TicketSupportSystem.Data.Entities;

namespace TicketSupportSystem.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(rel => rel.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasIndex(rel => rel.Email)
                   .IsUnique();
            builder.Property(rel => rel.Email)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(rel => rel.Password)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasMany(user => user.CreatedTickets)
                 .WithOne(ticket => ticket.User)
                 .HasForeignKey(ticket => ticket.UserId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(user => user.AssignedTickets)
                .WithOne(ticket => ticket.AssignedTo)
                .HasForeignKey(ticket => ticket.AssignedToId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
