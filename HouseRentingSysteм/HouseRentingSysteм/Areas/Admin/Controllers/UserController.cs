using HouseRentingSystem.Core.Constants;
using HouseRentingSystem.Core.Contracts.Admin;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSysteм.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        public async Task<IActionResult> All()
        {
            var model = await userService.All();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Forget([FromBody] string userId)
        {
            bool result = await userService.Forget(userId);

            if (result)
            {
                TempData[MessageConstants.SuccessMessage] = "User is now forgotten";
            }
            else
            {
                TempData[MessageConstants.ErrorMessage] = "User is unforgetable";
            }

            return RedirectToAction(nameof(All));
        }
    }
}
