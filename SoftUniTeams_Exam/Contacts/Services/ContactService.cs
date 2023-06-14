using Contacts.Contracts;
using Contacts.Data;
using Contacts.Data.Entities;
using Contacts.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Services
{
    public class ContactService : IContactService
    {
        private readonly ContactsDbContext contactContext;

        public ContactService(ContactsDbContext _contactContext)
        {
            contactContext = _contactContext;
        }

        public async Task AddContactToTeamAsync(string userId, int contactId)
        {
            bool isExistingContact = await contactContext
                .ApplicationUsersContacts
                .AnyAsync(uc => uc.ApplicationUserId == userId && uc.ContactId == contactId);

            if (isExistingContact == false) 
            {
                ApplicationUserContact applicationUser = new ApplicationUserContact()
                {
                    ApplicationUserId = userId,
                    ContactId = contactId
                };

                await contactContext.ApplicationUsersContacts.AddAsync(applicationUser);
                await contactContext.SaveChangesAsync();
            }
        }

        public async Task CreateContactAsync(FormContactViewModel contact)
        {
            Contact newContact = new Contact()
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Address = string.IsNullOrEmpty(contact.Address) ? "No address" : contact.Address,
                Website = contact.Website,
            };

            await contactContext.Contacts.AddAsync(newContact);
            await contactContext.SaveChangesAsync();
        }

        public async Task EditContactAsync(int contactId, FormContactViewModel model)
        {
            Contact? existingContact = await contactContext
                .Contacts
                .FindAsync(contactId);

            if (existingContact != null)
            {
                existingContact.FirstName = model.FirstName;
                existingContact.LastName = model.LastName;
                existingContact.Email = model.Email;
                existingContact.PhoneNumber = model.PhoneNumber;
                existingContact.Address = string.IsNullOrEmpty(model.Address) ? "No address" : model.Address;
                existingContact.Website = model.Website;

                await contactContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AllContactViewModel>> GetAllContactsAsync()
        {
            return await contactContext
                .Contacts
                .Select(c => new AllContactViewModel()
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Address = string.IsNullOrEmpty(c.Address) ? "No address" : c.Address,
                    Website = c.Website,
                    Id = c.Id
                })
                .ToListAsync();
        }

        public async Task<FormContactViewModel> GetEditFilledFormAsync(int contactId)
        {
            Contact? existingContact = await contactContext
                .Contacts
                .FirstOrDefaultAsync(c => c.Id == contactId);

            FormContactViewModel model = new FormContactViewModel();

            if (existingContact != null)
            {
                model.FirstName= existingContact.FirstName;
                model.LastName= existingContact.LastName;
                model.Email = existingContact.Email;
                model.PhoneNumber = existingContact.PhoneNumber;
                model.Address = string.IsNullOrEmpty(existingContact.Address)
                        ? "No address"
                        : existingContact.Address;
                model.Website = existingContact.Website;

                return model;
            }
            
            return model;
        }

        public async Task<IEnumerable<AllContactViewModel>> GetMyTeamContactsAsync(string userId)
        {
            return await contactContext
                .ApplicationUsersContacts
                .Where(uc => uc.ApplicationUserId == userId)
                .Select(uc => new AllContactViewModel()
                {
                    FirstName = uc.Contact.FirstName,
                    LastName = uc.Contact.LastName,
                    Email = uc.Contact.Email,
                    PhoneNumber = uc.Contact.PhoneNumber,
                    Address = string.IsNullOrEmpty(uc.Contact.Address) ? "No address" : uc.Contact.Address,
                    Website = uc.Contact.Website,
                    Id = uc.Contact.Id
                })
                .ToListAsync();
        }

        public async Task RemoveContactFromTeamAsync(string userId, int contactId)
        {
            var existingContact = await contactContext
                .ApplicationUsersContacts
                .FirstOrDefaultAsync(uc => uc.ApplicationUserId == userId && uc.ContactId == contactId);

            if (existingContact != null)
            {
                contactContext.ApplicationUsersContacts.Remove(existingContact);
                await contactContext.SaveChangesAsync();
            }
        }
    }
}
