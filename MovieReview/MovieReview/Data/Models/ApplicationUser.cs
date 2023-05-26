using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieReview.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(20)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }

        public List<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
    }
}
