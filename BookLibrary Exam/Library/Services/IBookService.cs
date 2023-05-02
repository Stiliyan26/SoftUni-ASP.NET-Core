using Library.Data.Models;
using Library.Models;

namespace Library.Services
{
    public interface IBookService
    {
        Task<ICollection<Category>> GetAllCategoriesAsync();

        Task AddBookAsync(AddBookViewModel model);

        Task<ICollection<BookViewModel>> GetAllBooksAsync();

        Task AddBookToCollectionAsync(string userId, int bookId);

        Task<ICollection<BookViewModel>> GetMineBooksAsync(string userId);

        Task RemoveBookFromCollectionAsync(string userId, int bookId);
    }
}
