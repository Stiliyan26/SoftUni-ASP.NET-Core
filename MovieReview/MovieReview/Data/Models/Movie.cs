using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReview.Data.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Director { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Genre { get; set; } = null!;

        [Required]
        public DateTime ReleaseDate { get; set; }

        [ForeignKey(nameof(Review))]
        public int? ReviewId { get; set; }

        public virtual Review Review { get; set; }

        public List<UserMovie> UsersMovies { get; set; }
    }
}
