using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext libaryContext;

        public BookService(LibraryDbContext _libaryContext)
        {
            libaryContext = _libaryContext;
        }

        public async Task AddBookToCollection(string userId, int bookId)
        {
            bool alreadyExists = await libaryContext
                .IdentityUsersBooks
                .AnyAsync(ub => ub.CollectorId == userId && ub.BookId == bookId);

            if (!alreadyExists)
            {
                IdentityUserBook userBook = new IdentityUserBook()
                {
                    CollectorId = userId,
                    BookId = bookId
                };

                await libaryContext.IdentityUsersBooks.AddAsync(userBook);
                await libaryContext.SaveChangesAsync();
            }
        }

        public async Task CreateBookAsync(BookViewModel book)
        {
            Book newBook = new Book()
            {
                Title = book.Title,
                Author= book.Author,
                Description= book.Description,
                ImageUrl = book.Url,
                Rating = book.Rating,
                CategoryId = book.CategoryId,
            };

            await libaryContext.Books.AddAsync(newBook);
            await libaryContext.SaveChangesAsync();
        }

        public async Task EditBookAsync(int id, BookViewModel book)
        {
            Book? existingBook = await libaryContext
                .Books
                .FindAsync(id);

            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.ImageUrl = book.Url;
                existingBook.Rating = book.Rating;
                existingBook.Description = book.Description;
                existingBook.CategoryId = book.CategoryId;

                await libaryContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AllBookViewModel>> GetAllBooks()
        {
            return await libaryContext
                .Books
                .Select(book => new AllBookViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Rating = book.Rating,
                    ImageUrl = book.ImageUrl,
                    Category = book.Category.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            return await libaryContext
                .Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<BookViewModel> GetEditBookViewModel(int bookId)
        {
            var categories = await libaryContext
                .Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            Book? existingBook = await libaryContext
                .Books
                .FirstOrDefaultAsync(b => b.Id == bookId);

            BookViewModel bookViewModel = new BookViewModel()
            {
                Categories = categories
            };

            if (existingBook != null)
            {
                bookViewModel.Title = existingBook.Title;
                bookViewModel.Author = existingBook.Author;
                bookViewModel.Rating = existingBook.Rating;
                bookViewModel.Description = existingBook.Description;
                bookViewModel.Url = existingBook.ImageUrl;
                bookViewModel.CategoryId = existingBook.CategoryId;
            }

            return bookViewModel;
        }

        public async Task<IEnumerable<AllBookViewModel>> GetMyBooks(string userId)
        {
            return await libaryContext
                .IdentityUsersBooks
                .Where(ub => ub.CollectorId == userId)
                .Select(ub => new AllBookViewModel()
                {
                    Id = ub.Book.Id,
                    Title = ub.Book.Title,
                    Author = ub.Book.Author,
                    Description = ub.Book.Description,
                    ImageUrl = ub.Book.ImageUrl,
                    Category = ub.Book.Category.Name
                })
                .ToListAsync();
        }

        public async Task RemoveBookFromCollection(string userId, int bookId)
        {
            IdentityUserBook? userBook = await libaryContext
                .IdentityUsersBooks
                .FirstOrDefaultAsync(ub => ub.CollectorId == userId && ub.BookId == bookId);

            if (userBook != null)
            {
                libaryContext.IdentityUsersBooks.Remove(userBook);
                await libaryContext.SaveChangesAsync();
            }
        }
    }
}
