﻿using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Extensions;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSysteм.Extensions;
using HouseRentingSysteм.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSysteм.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {
        private readonly IHouseService houseService;

        private readonly IAgentService agentService;

        private readonly ILogger logger;
        public HouseController(
            IHouseService _houseService,
            IAgentService _agentService,
            ILogger<HouseController> _logger)
        {
            houseService = _houseService;
            agentService = _agentService;
            logger = _logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]AllHousesQueryModel query)
        {
            var result = await houseService.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = result.TotalHousesCount;
            query.Categories =  await houseService.AllCategoriesNames();
            query.Houses = result.Houses;

            return View(query);
        }

        public async Task<IActionResult> Mine()
        {
            IEnumerable<HouseServiceModel> myHouses;
            var userId = User.Id();

            if (await agentService.ExistsById(userId))
            {
                int agentId = await agentService.GetAgentId(userId);
                myHouses = await houseService.AllHousesByAgentId(agentId);
            }
            else  
            {
                myHouses = await houseService.AllHousesByUserId(userId);    
            }

            return View(myHouses);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id, string information)
        {
            if ((await houseService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var model = await houseService.HousesDetailsById(id);

            if (information != model.GetInformation())
            {
                TempData["ErrorMessage"] = "Don't touch my slug!";

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if ((await agentService.ExistsById(User.Id())) == false)
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            var model = new HouseModel()
            {
                HouseCategories = await houseService.AllCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseModel model)
        {
            if ((await agentService.ExistsById(User.Id())) == false)
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if ((await houseService.CategoryExists(model.CategoryId)) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exists");
            }

            if (!ModelState.IsValid)
            {
                model.HouseCategories = await houseService.AllCategories();

                return View(model);
            }

            int agentId = await agentService.GetAgentId(User.Id());

            int id = await houseService.Create(model, agentId);

            return RedirectToAction(nameof(Details), new { id = id, information = model.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await houseService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if ((await houseService.HasAgentWithId(id, User.Id())) == false)
            {
                logger.LogInformation("User with id {0} attempted to access other agent house!", User.Id());

                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var house = await houseService.HousesDetailsById(id);
            var categoryId = await houseService.GetHouseCategoryId(house.Id);

            HouseModel model = new HouseModel()
            {
                Id = house.Id,
                Address = house.Address,
                CategoryId = categoryId,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                Title = house.Title,
                HouseCategories = await houseService.AllCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseModel model)
        {
            if (id != model.Id)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if ((await houseService.Exists(model.Id)) == false)
            {
                ModelState.AddModelError("", "House does not exist!");
                model.HouseCategories = await houseService.AllCategories();

                return View(model);
            }

            if ((await houseService.HasAgentWithId(model.Id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if ((await houseService.CategoryExists(model.CategoryId)) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
                model.HouseCategories = await houseService.AllCategories();

                return View(model);
            }

            if (!ModelState.IsValid)
            {
                model.HouseCategories = await houseService.AllCategories();

                return View(model);
            }

            await houseService.Edit(model.Id, model);

            return RedirectToAction(nameof(Details), new { model.Id, information = model.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await houseService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if ((await houseService.HasAgentWithId(id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var house = await houseService.HousesDetailsById(id);
            var model = new HouseDetailsViewModel()
            {
                Address = house.Address,
                ImageUrl = house.ImageUrl,
                Title = house.Title,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, HouseDetailsViewModel model)
        {
            if ((await houseService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if ((await houseService.HasAgentWithId(id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await houseService.Delete(id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if ((await houseService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if ((await agentService.ExistsById(User.Id())) == true)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }   

            if (await houseService.IsRented(id) == true)
            {
                return RedirectToAction(nameof(All));
            }
                
            await houseService.Rent(id, User.Id());

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if ((await houseService.Exists(id)) == false ||
                (await houseService.IsRented(id) == false))
            {
                return RedirectToAction(nameof(All));
            }

            if ((await houseService.IsRentedByUserWithId(id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await houseService.Leave(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
