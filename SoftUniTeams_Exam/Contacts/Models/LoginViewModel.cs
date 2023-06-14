
using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; } = null!;
    }
}
