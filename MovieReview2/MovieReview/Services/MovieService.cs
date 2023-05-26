﻿using Microsoft.EntityFrameworkCore;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Models;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;

        public MovieService(WatchlistDbContext _context)
        {
            context = _context;
        }

        public async Task AddMovieAsync(AddMovieViewModel model)
        {
            var entity = new Movie()
            {
                Director = model.Director,
                Title = model.Title,
                GenreId = model.GenreId,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
            };

            await context.Movies.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddMovieToCollectionAsync(int movieId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .ThenInclude(um => um.Movie)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID!");
            }

            var movie = await context.Movies
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                throw new ArgumentException("Invlaid movie ID!");
            }

            if (!user.UsersMovies.Any(um => um.MovieId == movieId))
            {
                UserMovie validUserMovie = new UserMovie()
                {
                    MovieId = movie.Id,
                    UserId = user.Id,
                    Movie = movie,
                    User = user
                };

                user.UsersMovies.Add(validUserMovie);

                await context.SaveChangesAsync();
            }
        }

        public async Task EditAsync(EditMovieViewModel model)
        {
            var entity = await context.Movies
                .FindAsync(model.Id);

            entity.Rating = model.Rating;
            entity.ImageUrl = model.ImageUrl;
            entity.Director = model.Director;
            entity.Title = model.Title;
            entity.GenreId = model.GenreId;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetAllAsync()
        {
            var entities = await context.Movies
                .Include(m => m.Genre)
                .ToListAsync();

            return entities
                .Select(m => new MovieViewModel
                {
                    Director = m.Director,
                    Genre = m?.Genre?.Name,
                    Id = m.Id,
                    ImageUrl = m.ImageUrl,
                    Rating = m.Rating,
                    Title = m.Title,
                });
        }

        public async Task<EditMovieViewModel> GetForEditAsync(int id)
        {
            var movie = await context.Movies
                .FindAsync(id);

            if (movie == null)
            {
                throw new ArgumentException("No such data...");
            }

            var model = new EditMovieViewModel()
            {
                Id = movie.Id,
                Director = movie.Director,
                GenreId = movie.GenreId,
                ImageUrl = movie.ImageUrl,
                Rating = movie.Rating,
                Title = movie.Title
            };

            model.Genres = await GetGenresAsync();

            return model;
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await context
                .Genres
                .ToListAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetWatchedMoviesAsync(string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .ThenInclude(um => um.Movie)
                .ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID!");
            }

            return user.UsersMovies
                .Select(m => new MovieViewModel()
                {
                    Id = m.Movie.Id,
                    Title = m.Movie.Title,
                    Director = m.Movie.Director,
                    ImageUrl = m.Movie.ImageUrl,
                    Rating = m.Movie.Rating,
                    Genre = m.Movie?.Genre?.Name,
                });
        }

        public async Task RemoveMovieFromCollectionAsync(int movieId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .ThenInclude(um => um.Movie)
                .ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID!");
            }

            var movie = user.UsersMovies
                .FirstOrDefault(m => m.Movie.Id == movieId);

            if (movie != null)
            {
                user.UsersMovies.Remove(movie);

                await context.SaveChangesAsync();
            }
        }
    }
}
