using Homies.Models;
using Microsoft.AspNetCore.Identity;

namespace Homies.Contracts
{
    public interface IEventService
    {
        Task<IEnumerable<AllEventViewModel>> GetAllEventsAsync();

        Task<IEnumerable<TypeViewModel>> GetAllTypesAsync();

        Task CreateEventAsync(FormEventViewModel model, IdentityUser organizer);

        Task JoinEventAsync(string userId, int eventId);

        Task<IEnumerable<AllEventViewModel>> GetJoinedEventsAsync(string userId);

        Task<bool> IsAlreadyEventJoinedByUser(string userId, int eventId);

        Task LeaveEventAsync(string userId, int eventId);

        Task<FormEventViewModel> GetFilledEditEventFormAsync(int eventId);

        Task EditEventAsync(int id , FormEventViewModel eventModel);

        Task<DetailsViewModel?> GetDetailsOfEventViewModelAsync(int eventId);
    }
}
