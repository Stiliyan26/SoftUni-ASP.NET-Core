using Contacts.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data
{
    public class ContactsDbContext : IdentityDbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUserContact> ApplicationUsersContacts { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserContact>(entity =>
            {
                entity
                    .HasKey(e => new { e.ApplicationUserId, e.ContactId });
            });

            builder
               .Entity<Contact>()
               .HasData(new Contact()
               {
                   Id = 1,
                   FirstName = "Bruce",
                   LastName = "Wayne",
                   PhoneNumber = "+359881223344",
                   Address = "Gotham City",
                   Email = "imbatman@batman.com",
                   Website = "www.batman.com"
               });


            base.OnModelCreating(builder);
        }
    }
}