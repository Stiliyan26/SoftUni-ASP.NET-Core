using Library.Data.Models;
using Library.Models;

namespace Library.Services
{
    public interface IBookService
    {
        Task<ICollection<Category>> GetAllCategoriesAsync();

        Task AddBookAsync(AddBookViewModel model);

        Task<ICollection<BookViewModel>> GetAllBooksAsync();
    }
}
