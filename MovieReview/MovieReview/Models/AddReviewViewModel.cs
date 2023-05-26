using MovieReview.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models
{
    public class AddReviewViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(typeof(decimal), "0.0", "10.0")]
        public decimal Rating { get; set; }

        [Required]
        [MaxLength(2147483647)]
        public string Comment { get; set; } = null!;

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
