using System.ComponentModel.DataAnnotations;

namespace ForumApp.Data.Models
{
    public class AddPostViewModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Filed {0} is required!")]
        [StringLength(100, MinimumLength = 10,
            ErrorMessage = "Filed {0} should be between {2} and {1} characters long!")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "Filed {0} is required!")]
        [StringLength(1500, MinimumLength = 30,
            ErrorMessage = "Filed {0} should be between {2} and {1} characters long!")]
        public string Content { get; set; }
    }
}
