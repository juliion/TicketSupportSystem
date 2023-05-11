using Microsoft.EntityFrameworkCore;
using TicketSupportSystem.Data.Entities;

namespace TicketSupportSystem.Data
{
    public class TicketSupportSystemContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public TicketSupportSystemContext(DbContextOptions<TicketSupportSystemContext> opt)
            : base(opt) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(user => user.CreatedTickets)
                .WithOne(ticket => ticket.User)
                .HasForeignKey(ticket => ticket.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(user => user.AssignedTickets)
                .WithOne(ticket => ticket.AssignedTo)
                .HasForeignKey(ticket => ticket.AssignedToId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
