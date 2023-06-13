using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext libaryContext;

        public BookService(LibraryDbContext _libaryContext)
        {
            this.libaryContext = _libaryContext;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            Book book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.Url,
                Rating = model.Rating,
                CategoryId = model.CategoryId
            };

            await libaryContext.Books.AddAsync(book);
            await libaryContext.SaveChangesAsync();
        }

        public async Task AddBookToCollectionAsync(string userId, int bookId)
        {
            bool alreadyAddedBook = await libaryContext
                .IdentityUsersBooks
                .AnyAsync(ub => ub.CollectorId == userId && ub.BookId == bookId);

            if (alreadyAddedBook == false)
            {
                var userBook = new IdentityUserBook()
                {
                    CollectorId = userId,
                    BookId = bookId
                };

                await libaryContext.IdentityUsersBooks.AddAsync(userBook);
                await libaryContext.SaveChangesAsync();
            }
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

        public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
        {
            return await libaryContext
                .Books
                .Select(b => new AllBookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Category = b.Category.Name
                })
                .ToListAsync();
        }

        public async Task<BookViewModel?> GetBookByIdAsync(int bookId)
        {
            return await libaryContext
                .Books
                .Where(b => b.Id == bookId)
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    CategoryId = b.CategoryId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MineBookViewModel>> GetMyBooksAsync(string userId)
        {
            return await libaryContext
                .IdentityUsersBooks
                .Where(ub => ub.CollectorId == userId)
                .Select(ub => new MineBookViewModel()
                {
                    Id = ub.Book.Id,
                    Title = ub.Book.Title,
                    Author = ub.Book.Author,
                    ImageUrl = ub.Book.ImageUrl,
                    Description = ub.Book.Description,
                    Category = ub.Book.Category.Name
                })
                .ToListAsync();
        }

        public async Task RemoveBookFromCollectionAsync(string userId, int bookId)
        {
            var identityUserBook = await libaryContext
                .IdentityUsersBooks
                .FirstOrDefaultAsync(ub => ub.BookId == bookId && ub.CollectorId == userId);

            if (identityUserBook != null)
            {
                libaryContext.IdentityUsersBooks.Remove(identityUserBook);
                await libaryContext.SaveChangesAsync();
            }
        }

        public async Task<AddBookViewModel?> GetBookByIdForEditAsync(int id)
        {
            var categories = await libaryContext
                .Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return await libaryContext
                .Books
                .Where(b => b.Id == id)
                .Select(b => new AddBookViewModel()
                {
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    Url = b.ImageUrl,
                    Rating = b.Rating,
                    CategoryId = b.CategoryId,
                    Categories = categories
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditBookAsync(AddBookViewModel model, int id)
        {
            var book = await libaryContext
                .Books
                .FindAsync(id);

            if (book != null)
            {
                book.Title = model.Title;
                book.Author = model.Author;
                book.Description = model.Description;
                book.ImageUrl = model.Url;
                book.Rating = model.Rating;
                book.CategoryId = model.CategoryId;

                await libaryContext.SaveChangesAsync();
            }
        }
    }
}
