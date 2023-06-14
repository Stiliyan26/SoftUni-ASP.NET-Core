using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Contacts.Data.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(60)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(13)]
        //[RegularExpression(@"^(\+359|0)((\s|-|\s*)\d{3})((\s|-|\s*)\d{2})((\s|-|\s*)\d{2})((\s|-|\s*)\d{2})$")]
        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; } 

        //[RegularExpression(@"^www.[A-Za-z0-9-]+.bg$")]
        [Required]
        public string Website { get; set; } = null!;

        public IEnumerable<ApplicationUserContact> ApplicationUsersContacts { get; set; } = new List<ApplicationUserContact>();
    }
}
