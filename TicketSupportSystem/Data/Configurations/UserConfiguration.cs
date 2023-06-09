﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TicketSupportSystem.Data.Entities;

namespace TicketSupportSystem.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(user => user.Surname)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasMany(user => user.CreatedTickets)
                 .WithOne(ticket => ticket.User)
                 .HasForeignKey(ticket => ticket.UserId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(user => user.AssignedTickets)
                .WithOne(ticket => ticket.AssignedTo)
                .HasForeignKey(ticket => ticket.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(user => user.Comments)
                 .WithOne(comment => comment.User)
                 .HasForeignKey(comment => comment.UserId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
