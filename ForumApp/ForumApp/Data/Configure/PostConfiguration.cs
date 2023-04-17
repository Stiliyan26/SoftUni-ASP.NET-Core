using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configure
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            List<Post> posts = GetPosts();

            builder.HasData(posts);
        }

        private List<Post> GetPosts()
        {
            return new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first post will be about performing Crud operations in MVC"
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second post will be about performing Crud operations in MVC"
                },
                new Post()
                {
                    Id = 3,
                    Title = "My third post",
                    Content = "My third post will be about performing Crud operations in MVC"
                }
            };
        }
    }
}
