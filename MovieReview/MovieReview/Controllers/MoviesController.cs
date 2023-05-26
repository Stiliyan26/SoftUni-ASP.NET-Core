using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReview.Contracts;
using MovieReview.Models;
using MovieReview.Services;

namespace MovieReview.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService _movieService)
        {
            movieService = _movieService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await movieService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddMovieViewModel model = new AddMovieViewModel();
           
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await movieService.AddMovieAsync(model);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Somthing went wrong!");

                return View(model);
            }
        }

        public async Task<IActionResult> Details(int movieId)
        {
            var model = await movieService.GetMovieByIdAsync(movieId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddReview()
        {
            AddReviewViewModel model = new AddReviewViewModel();

            return View(model);
        }
    }
}
