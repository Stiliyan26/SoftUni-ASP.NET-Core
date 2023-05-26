using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReview.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Rating { get; set; }

        [Required]
        [MaxLength(2147483647)]
        public string Comment { get; set; } = null!;

        [Required]
        public DateTime DateCreated { get; set; }

        [ForeignKey(nameof(Movie))]
        public int? MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
