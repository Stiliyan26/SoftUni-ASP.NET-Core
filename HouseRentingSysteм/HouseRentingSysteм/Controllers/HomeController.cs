using HouseRentingSystem.Core.Contracts;
using HouseRentingSysteм.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using static HouseRentingSysteм.Areas.Admin.Constants.AdminConstants;

namespace HouseRentingSysteм.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseService houseService;

        private readonly ILogger logger;
        public HomeController(IHouseService _houseService, ILogger<HomeController> _logger)
        {
            houseService = _houseService;
            logger = _logger;
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
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature.Error.Message;

            logger.LogError(feature.Error, "TraceIndentifier: {0}", HttpContext.TraceIdentifier);

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}