using System.ComponentModel.DataAnnotations;

namespace Homies.Data.Entities
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Name { get; set; } = null!;

        public IEnumerable<Event> Event { get; set; }
            = new List<Event>();
    }
}
