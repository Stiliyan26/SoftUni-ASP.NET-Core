using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();

        Task<IEnumerable<MineBookViewModel>> GetMyBooksAsync(string userId);

        Task<BookViewModel?> GetBookByIdAsync(int bookId);

        Task AddBookToCollectionAsync(string userId, int bookId);

        Task RemoveBookFromCollectionAsync(string userId, int bookId);

        Task AddBookAsync(AddBookViewModel model);

        Task<IEnumerable<CategoryViewModel>> GetAllCategories();

        Task<AddBookViewModel?> GetBookByIdForEditAsync(int id);

        Task EditBookAsync(AddBookViewModel model, int id);
    }
}
