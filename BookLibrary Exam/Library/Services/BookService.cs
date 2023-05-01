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
    }
}
