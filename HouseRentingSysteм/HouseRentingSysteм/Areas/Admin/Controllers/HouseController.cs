using HouseRentingSystem.Core.Contracts;
using HouseRentingSysteм.Areas.Admin.Models;
using HouseRentingSysteм.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSysteм.Areas.Admin.Controllers
{
    public class HouseController : BaseController
    {
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;
        public HouseController(IHouseService _houseService, IAgentService _agentService)
        {
            houseService = _houseService;
            agentService = _agentService;
        }

        public async Task<IActionResult> Mine()
        {
            MyHousesViewModel myHouses = new MyHousesViewModel();
            var adminId = User.Id();
            myHouses.RentedHouses = await houseService.AllHousesByUserId(adminId);
            var agentId = await agentService.GetAgentId(adminId);
            myHouses.AddedHouses = await houseService.AllHousesByAgentId(agentId);

            return View(myHouses);
        }
    }
}
