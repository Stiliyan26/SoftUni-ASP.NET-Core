using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService bookService;

        public BooksController(IBookService _bookService)
        {
            bookService = _bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddBookViewModel model = new AddBookViewModel()
            {
                Categories = await bookService.GetAllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await bookService.AddBookAsync(model);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong!");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await bookService.GetAllBooksAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int bookId)
        {
            try
            {
                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                    .Value;

                await bookService.AddBookToCollectionAsync(userId, bookId);
            }
            catch (Exception)
            {
                throw new ArgumentException("Something in went wrong!");
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var userId = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                .Value;

            var myBooks = await bookService.GetMineBooksAsync(userId);


            return View("Mine", myBooks);
        }

        [HttpPost]

        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            try
            {
                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                    .Value;

                await bookService.RemoveBookFromCollectionAsync(userId, bookId);
            }
            catch (Exception)
            {

                throw new ArgumentException("Something in went wrong!");
            }

            return RedirectToAction(nameof(Mine));
        }
    }
}
