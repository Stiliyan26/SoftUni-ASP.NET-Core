using Microsoft.AspNetCore.Identity;

namespace Contacts.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<ApplicationUserContact> ApplicationUsersContacts { get; set; }
            = new List<ApplicationUserContact>();
    }
}
