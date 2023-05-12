using Microsoft.EntityFrameworkCore;
using TicketSupportSystem.Data.Configurations;
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
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
        }
    }
}
