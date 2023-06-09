﻿using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Exceptions;
using HouseRentingSystem.Core.Models.Agent;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebShopDemo.Core.Data.Common;

namespace HouseRentingSystem.Core.Services
{
    public class HouseService : IHouseService
    {
        private readonly IRepository repo;

        private readonly IGuard guard;

        private readonly ILogger logger;    
        public HouseService(
            IRepository _repo,
            IGuard _guard,
            ILogger<HouseService> _logger)
        {
            repo = _repo;
            guard = _guard;
            logger = _logger;
        }

        public async Task<HousesQueryModel> All(
            string? category = null,
            string? searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1)
        {
            var result = new HousesQueryModel();

            var houses = repo.AllReadonly<House>()
                .Where(h => h.IsActive);

            if (string.IsNullOrEmpty(category) == false)
            {
                houses = houses
                    .Where(h => h.Category.Name == category);
            }

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                houses = houses
                    .Where(h => EF.Functions.Like(h.Title.ToLower(), searchTerm) ||
                        EF.Functions.Like(h.Address.ToLower(), searchTerm) ||
                        EF.Functions.Like(h.Description.ToLower(), searchTerm));           
            }

            houses = sorting switch
            {
                HouseSorting.Price => houses
                    .OrderBy(h => h.PricePerMonth),

                HouseSorting.NotRentedFirst => houses
                    .OrderBy(h => h.RenterId),

                _ => houses
                    .OrderByDescending(h => h.Id)
            };

            result.Houses = await houses
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(h => new HouseServiceModel()
                {
                    Address = h.Address,
                    Id = h.Id,
                    ImageUrl = h.ImageUrl,
                    IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth,
                    Title = h.Title
                })
                .ToListAsync();

            result.TotalHousesCount = await houses.CountAsync();

            return result; 
        }

        public async Task<IEnumerable<HouseCategoryModel>> AllCategories()
        {
            return await repo.AllReadonly<Category>()
                .OrderBy(c => c.Name)
                .Select(c => new HouseCategoryModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoriesNames()
        {
            return await repo.AllReadonly<Category>()
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(int id)
        {
            return await repo.AllReadonly<House>()
                .Where(h => h.AgentId == id && h.IsActive)
                .Select(h => new HouseServiceModel()
                {
                    Id = h.Id,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    IsRented = (h.RenterId != null) ? true : false,
                    PricePerMonth = h.PricePerMonth,
                    Title = h.Title
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(string userId)
        {
            return await repo.AllReadonly<House>()
                .Where(h => h.RenterId == userId && h.IsActive)
                .Select(h => new HouseServiceModel()
                {
                    Id = h.Id,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    IsRented = (h.RenterId != null) ? true : false,
                    PricePerMonth = h.PricePerMonth,
                    Title = h.Title
                })
                .ToListAsync();
        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            return await repo.AllReadonly<Category>()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> Create(HouseModel model, int agentId)
        {
            var house = new House()
            {
                Address = model.Address,
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                Title = model.Title,
                AgentId = agentId
            };

            try
            {
                await repo.AddAsync(house);
                await repo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(nameof(Create), ex);
                throw new ApplicationException("Database failed to save info!", ex);
            }

            return house.Id;
        }

        public async Task Delete(int houseId)
        {
            var house = await repo.GetByIdAsync<House>(houseId);
            house.IsActive = false;

            await repo.SaveChangesAsync();
        }

        public async Task Edit(int houseId, HouseModel model)
        {
            House existingHouse = await repo
                .GetByIdAsync<House>(houseId);

            if (existingHouse != null) 
            {
                existingHouse.Description = model.Description;
                existingHouse.ImageUrl = model.ImageUrl;
                existingHouse.PricePerMonth = model.PricePerMonth;
                existingHouse.Title = model.Title;
                existingHouse.Address = model.Address;
                existingHouse.CategoryId = model.CategoryId;

                await repo.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            return await repo.AllReadonly<House>()
                .AnyAsync(h => h.Id == id && h.IsActive);
        }

        public async Task<int> GetHouseCategoryId(int houseId)
        {
            return (await repo.GetByIdAsync<House>(houseId)).CategoryId;
        }

        public async Task<bool> HasAgentWithId(int houseId, string currentUserId)
        {
            return await repo.AllReadonly<House>()
                .Include(h => h.Agent)
                .AnyAsync(h => h.Agent.UserId == currentUserId 
                    && h.Id == houseId 
                    && h.IsActive);
        }

        public async Task<HouseDetailsModel> HousesDetailsById(int id)
        {
            return await repo.AllReadonly<House>()
                .Where(h => h.Id == id && h.IsActive)
                .Select(h => new HouseDetailsModel()
                {
                    Id = h.Id,
                    Address = h.Address,
                    Category = h.Category.Name,
                    Description = h.Description,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    Title = h.Title,
                    IsRented =  h.RenterId != null,
                    Agent = new AgentServiceModel()
                    {
                        Email = h.Agent.User.Email,
                        PhoneNumber = h.Agent.PhoneNumber,
                    }
                })
                .FirstAsync();
        }

        public async Task<bool> IsRented(int houseId)
        {
            return (await repo.GetByIdAsync<House>(houseId)).RenterId != null;
        }

        public async Task<bool> IsRentedByUserWithId(int houseId, string currentUserId)
        {
            return await repo.AllReadonly<House>()
                .AnyAsync(h => h.RenterId == currentUserId 
                    && h.Id == houseId 
                    && h.IsActive);
        }

        public async Task<IEnumerable<HouseHomeModel>> LastThreeHouses()
        {
            return await repo.AllReadonly<House>()
                .Where(h => h.IsActive)
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseHomeModel()
                {
                    Id = h.Id,
                    ImageUrl = h.ImageUrl,
                    Title = h.Title,
                    Address = h.Address
                })
                .Take(3)
                .ToListAsync();
        }

        public async Task Leave(int houseId)
        {
            House? house = await repo.GetByIdAsync<House>(houseId);

            if (house != null && house.RenterId == null)
            {
                throw new ArgumentException("House is not rented!");
            }

            guard.AgainstNull(house, "House can't be found!");
            house.RenterId = null;

            await repo.SaveChangesAsync();
        }

        public async Task Rent(int houseId, string currentUserID)
        {
            House? house = await repo.GetByIdAsync<House>(houseId);

            if (house != null && house.RenterId != null)
            {
                throw new ArgumentException("House is already rented!");
            }

            guard.AgainstNull(house, "House can't be found!");
            house.RenterId = currentUserID;

            await repo.SaveChangesAsync();
        }
    }
}
