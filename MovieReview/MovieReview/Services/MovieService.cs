using Microsoft.EntityFrameworkCore;
using MovieReview.Contracts;
using MovieReview.Data;
using MovieReview.Data.Models;
using MovieReview.Models;

namespace MovieReview.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext context;

        public MovieService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task AddMovieAsync(AddMovieViewModel model)
        {
            var entity = new Movie()
            {
                Title = model.Title,
                Director = model.Director,
                Genre = model.Genre,
                ReleaseDate = model.ReleaseDate
            };

            await context.Movies.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetAllAsync()
        {
            var entities = await context
                .Movies
                .ToListAsync();

            return entities
                .Select(m => new MovieViewModel
                {
                    Title = m.Title,
                    Director = m.Director,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate
                });
        }
    }
}
