using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [UIHint("hidden")]
        public string? ReturnUrl { get; set; }
    }
}
