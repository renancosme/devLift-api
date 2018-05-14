using Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevLiftApp.Persistence
{
    public class DevLiftContext : DbContext
    {
        public DevLiftContext(DbContextOptions<DevLiftContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Event>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Event>()
                .Property(e => e.When)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
