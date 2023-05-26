using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Watchlist.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            UsersMovies = new List<UserMovie>();
        }

        /*[Key]
        public string Id { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(60)]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; }*/

        public List<UserMovie> UsersMovies { get; set; }
    }
}
