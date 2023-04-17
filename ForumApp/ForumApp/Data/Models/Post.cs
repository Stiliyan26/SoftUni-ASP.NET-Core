using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ForumApp.GlobalConstants.DataConstants.Post;

namespace ForumApp.Data.Models
{
    [Comment("Published posts")]
    public class Post
    {
        [Key]
        [Comment("Post's identifier")]
        public int Id { get; set; }


        [Comment("Post's title")]
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;


        [Comment("Post's content")]
        [Required]
        [MaxLength(ContextMaxLength)]
        public string Content { get; set; } = null!;

        [Comment("Mark record as deleted")]
        [Required]
        public bool IsDeleted { get; set; } = true;
    }
}
