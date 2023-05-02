using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;

        public BookService(LibraryDbContext _context)
        {
            context = _context;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            Book book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                CategoryId = model.CategoryId
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task AddBookToCollectionAsync(string userId, int bookId)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id!");
            }

            var book = await context.Books
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                throw new ArgumentException("Invalid book Id!");
            }

            ApplicationUserBook applicationUserBook = new ApplicationUserBook()
            {
                ApplicationUser = user,
                ApplicationUserId = userId,
                Book = book,
                BookId = bookId
            };

            if (!user.ApplicationUsersBooks.Any(aub => aub.BookId == bookId))
            {
                user.ApplicationUsersBooks.Add(applicationUserBook);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<BookViewModel>> GetAllBooksAsync()
        {
            ICollection<Book> books = await context
                .Books
                .Include(b => b.Category)
                .ToListAsync();

            ICollection<BookViewModel> bookViewModels = books
                .Select(book => new BookViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    ImageUrl = book.ImageUrl,
                    Rating = book.Rating,
                    Category = book.Category?.Name
                })
                .ToList();

            return bookViewModels;
        }

        public async Task<ICollection<Category>> GetAllCategoriesAsync()
        {
            return await context
                .Categories
                .ToListAsync();
        }

        public async Task<ICollection<BookViewModel>> GetMineBooksAsync(string userId)
        {
            var user = await context.Users
                .Include(u => u.ApplicationUsersBooks)
                .ThenInclude(aub => aub.Book)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var books = user.ApplicationUsersBooks
                .Select(book => new BookViewModel()
                {
                    Id = book.Book.Id,
                    Title = book.Book.Title,
                    Author = book.Book.Author,
                    Description = book.Book.Description,
                    ImageUrl = book.Book.ImageUrl,
                    Rating = book.Book.Rating,
                    Category = book.Book?.Category?.Name
                })
                .ToList();

            return books;
        }

        public async Task RemoveBookFromCollectionAsync(string userId, int bookId)
        {
            var user = context.Users
                .Include(u => u.ApplicationUsersBooks)
                .ThenInclude(aub => aub.Book)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id!");
            }

            var book = user.ApplicationUsersBooks
                .FirstOrDefault(b => b.Book.Id == bookId);

            if (book != null)
            {
                user.ApplicationUsersBooks.Remove(book);
                await context.SaveChangesAsync();
            }
        }
    }
}
