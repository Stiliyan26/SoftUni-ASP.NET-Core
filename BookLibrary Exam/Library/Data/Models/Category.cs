using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new List<Book>();    
    }
}
