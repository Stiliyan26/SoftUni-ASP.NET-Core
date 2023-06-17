using Homies.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Homies.Data
{
    public class HomiesDbContext : IdentityDbContext
    {
        public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventParticipant> EventParticipants { get; set; }

        public DbSet<Entities.Type> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventParticipant>(e =>
            {
                e.HasKey(e => new { e.HelperId, e.EventId });
                e.HasOne(ep => ep.Event)
                    .WithMany(e => e.EventsParticipants)
                    .HasForeignKey(ep => ep.EventId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
                
            
            modelBuilder
                .Entity<Entities.Type>()
                .HasData(new Entities.Type()
                {
                    Id = 1,
                    Name = "Animals"
                },
                new Entities.Type()
                {
                    Id = 2,
                    Name = "Fun"
                },
                new Entities.Type()
                {
                    Id = 3,
                    Name = "Discussion"
                },
                new Entities.Type()
                {
                    Id = 4,
                    Name = "Work"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}