using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }
    }
}
