using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReview.Data.Models
{
    public class UserMovie
    {
        [ForeignKey(nameof(User))]
        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Movie))]
        [Required]
        public int MovieId { get; set; }

        [Required]
        public Movie Movie { get; set; }
    }
}
