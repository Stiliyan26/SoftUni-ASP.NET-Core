using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
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
        public HouseService(IRepository _repo)
        {
            repo = _repo;
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

        public async Task<bool> CategoryExists(int categoryId)
        {
            return await repo.AllReadonly<Category>()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> Create(HouseModel model)
        {
            var house = new House()
            {
                Address = model.Address,
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                Title = model.Title,
            };

            await repo.AddAsync(house);
            await repo.SaveChangesAsync();

            return house.Id;
        }

        public async Task<IEnumerable<HouseHomeModel>> LastThreeHouses()
        {
            return await repo.AllReadonly<House>()
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseHomeModel()
                {
                    Id = h.Id,
                    ImageUrl = h.ImageUrl,
                    Title = h.Title
                })
                .Take(3)
                .ToListAsync();
        }
    }
}
