using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Agent;
using HouseRentingSysteм.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSysteм.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService _agentService)
        {
            this.agentService = _agentService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            User.Id();

            if (await agentService.ExistsById(User.Id()))
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentModel model)
        {
            return RedirectToAction("All", "House");
        }
    }
}
