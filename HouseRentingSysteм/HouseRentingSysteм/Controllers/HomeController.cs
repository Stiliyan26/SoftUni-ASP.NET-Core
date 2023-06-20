using HouseRentingSystem.Core.Contracts;
using HouseRentingSysteм.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static HouseRentingSysteм.Areas.Admin.Constants.AdminConstants;

namespace HouseRentingSysteм.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseService houseService;
        public HomeController(IHouseService _houseService)
        {
            this.houseService = _houseService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }

            var model = await houseService.LastThreeHouses();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}