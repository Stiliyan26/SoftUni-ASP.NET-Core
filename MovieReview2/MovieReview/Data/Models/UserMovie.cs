using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Watchlist.Data.Models
{
    public class UserMovie
    {
        [ForeignKey(nameof(User))]
        [Required]

        public string UserId  { get; set; }

        [Required]
        public User User { get; set; }

        [ForeignKey(nameof(Movie))]
        [Required]
        public int MovieId  { get; set; }

        [Required]
        public Movie Movie { get; set; }
    }
}