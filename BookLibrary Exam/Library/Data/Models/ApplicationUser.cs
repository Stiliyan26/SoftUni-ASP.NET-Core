using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Library.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<ApplicationUserBook> ApplicationUsersBooks { get; set; } 
            = new List<ApplicationUserBook>();
    }
}
