using Watchlist.Data.Models;
using Watchlist.Models;

namespace Watchlist.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetAllAsync();

        Task<IEnumerable<Genre>> GetGenresAsync();

        Task AddMovieAsync(AddMovieViewModel model);
    }
}
