using Library.Contracts;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService _bookService)
        {
            bookService = _bookService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await bookService.GetAllBooks();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            string userId = GetUserId();

            var model = await bookService.GetMyBooks(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            string userId = GetUserId();

            await bookService.AddBookToCollection(userId, id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            string userId = GetUserId();

            await bookService.RemoveBookFromCollection(userId, id);

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await bookService.GetAllCategories();

            BookViewModel model = new BookViewModel()
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            await bookService.CreateBookAsync(book);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await bookService.GetEditBookViewModel(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await bookService.EditBookAsync(id, model);

            return RedirectToAction(nameof(All));
        }
    }
}
