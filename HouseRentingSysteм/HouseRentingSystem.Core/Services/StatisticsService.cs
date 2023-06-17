using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Statistics;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using WebShopDemo.Core.Data.Common;


namespace HouseRentingSystem.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository repo;

        public StatisticsService(IRepository _repo)
        {
            repo = _repo;
        }
        public async Task<StatisticsServiceModel> Total()
        {
            int totalHouses = await repo.AllReadonly<House>()
                .CountAsync(h => h.IsActive);

            int rentedHouses = await repo.AllReadonly<House>()
                .CountAsync(h => h.IsActive && h.RenterId != null);

            return new StatisticsServiceModel()
            {
                TotalHouses = totalHouses,
                TotalRents = rentedHouses
            };
        }
    }
}
