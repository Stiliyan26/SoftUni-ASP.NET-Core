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
        public async Task<bool> ExistsById(string userId)
        {
            return await repo.All<Agent>()
                .AnyAsync(a => a.UserId == userId);
        }
    }
}
