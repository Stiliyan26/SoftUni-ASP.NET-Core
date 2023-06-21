using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Models.Admin
{
    public class UserServiceModel
    {
        public string UserId { get; set; } = null!;

        public string Email { get; init; } = null!;

        public string FullName { get; set; } = null!;

        public string? PhoneNumber { get; set; } = null;
    }
}
