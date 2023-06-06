using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDemo.Core.Data.Common;

namespace HouseRentingSystem.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository repo;

        public AgentService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task Create(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            await repo.AddAsync(agent);
            await repo.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(string userId)
        {
            return await repo.All<Agent>()
                .AnyAsync(a => a.UserId == userId);
        }

        public async Task<int> GetAgentId(string userId)
        {
            return (await repo.AllReadonly<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId))?.Id ?? 0 ;
        }

        public async Task<bool> UserHasRents(string userId)
        {
            return await repo.All<House>()
                .AnyAsync(h => h.RenterId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
        {
            return await repo.All<Agent>()
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
