using Contacts.Contracts;
using Contacts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class ContactsController : BaseController
    {
        private IContactService contactService;
        public ContactsController(IContactService _contactService)
        {
            contactService = _contactService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await contactService.GetAllContactsAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            FormContactViewModel model = new FormContactViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FormContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await contactService.CreateContactAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Team()
        {
            string userId = GetUserId();

            IEnumerable<AllContactViewModel> model = await contactService.GetMyTeamContactsAsync(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToTeam(int contactId)
        {
            string userId = GetUserId();

            await contactService.AddContactToTeamAsync(userId, contactId);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromTeam(int contactId)
        {
            string userId = GetUserId();

            await contactService.RemoveContactFromTeamAsync(userId, contactId);

            return RedirectToAction(nameof(Team));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            FormContactViewModel model = await contactService.GetEditFilledFormAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FormContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await contactService.EditContactAsync(id, model);

            return RedirectToAction(nameof(Team));
        }
    }
}
