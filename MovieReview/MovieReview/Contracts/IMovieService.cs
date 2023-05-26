using MovieReview.Models;

namespace MovieReview.Contracts
{
    public interface IMovieService
    {
        Task AddMovieAsync(AddMovieViewModel model);

        Task<IEnumerable<MovieViewModel>> GetAllAsync();

        Task<MovieViewModel> GetMovieByIdAsync(int id);
    }
}
