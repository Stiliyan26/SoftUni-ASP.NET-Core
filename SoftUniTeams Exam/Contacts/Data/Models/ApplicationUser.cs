using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Contacts.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<ApplicationUserContact> ApplicationUsersContacts { get; set; } 
            = new List<ApplicationUserContact>();
    }
}
