using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModel>> GetAllBooks();

        Task<IEnumerable<AllBookViewModel>> GetMyBooks(string userId);

        Task AddBookToCollection(string userId, int bookId);

        Task RemoveBookFromCollection(string userId, int bookId);

        Task<IEnumerable<CategoryViewModel>> GetAllCategories();

        Task CreateBookAsync(BookViewModel book);

        Task<BookViewModel> GetEditBookViewModel(int bookId);

        Task EditBookAsync(int id, BookViewModel book);
    }
}
