using Homies.Contracts;
using Homies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Homies.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;
        private readonly UserManager<IdentityUser> userManager;

        public EventController(
            IEventService _eventService,
            UserManager<IdentityUser> _userManager)
        {
            eventService = _eventService;
            userManager = _userManager;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await eventService.GetAllEventsAsync();

            return View(model); 
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var types = await eventService.GetAllTypesAsync();

            var model = new FormEventViewModel()
            {
                Types = types,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FormEventViewModel model)
        {
            string userId = GetUserId();
            IdentityUser organizer = await userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await eventService.CreateEventAsync(model, organizer);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            string userId = GetUserId();

            bool alreadyJoined = await eventService.IsAlreadyEventJoinedByUser(userId, id);

            if (alreadyJoined)
            {
                return RedirectToAction(nameof(All));
            }

            await eventService.JoinEventAsync(userId, id);

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();


            var model = await eventService.GetJoinedEventsAsync(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            string userId = GetUserId();

            await eventService.LeaveEventAsync(userId, id);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            FormEventViewModel model = await eventService.GetFilledEditEventFormAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FormEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await eventService.EditEventAsync(id, model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            DetailsViewModel? model = await eventService.GetDetailsOfEventViewModelAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }
    }
}
