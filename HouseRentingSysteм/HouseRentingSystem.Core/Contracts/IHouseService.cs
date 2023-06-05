using HouseRentingSystem.Core.Models.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Contracts
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseHomeModel>> LastThreeHouses();

        Task<IEnumerable<HouseCategoryModel>> AllCategories();

        Task<bool> CategoryExists(int categoryId);

        Task<int> Create(HouseModel model);
    }
}
