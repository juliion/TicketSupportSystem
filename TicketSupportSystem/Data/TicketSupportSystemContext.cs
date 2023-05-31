using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketSupportSystem.Data.Configurations;
using TicketSupportSystem.Data.Entities;

namespace TicketSupportSystem.Data
{
    public class TicketSupportSystemContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public TicketSupportSystemContext(DbContextOptions<TicketSupportSystemContext> opt)
            : base(opt) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
        }
    }
}
