using Homies.Contracts;
using Homies.Data;
using Homies.Data.Entities;
using Homies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Homies.Services
{
    public class EventService : IEventService
    {
        private readonly HomiesDbContext homieContext;

        public EventService(HomiesDbContext _homieContext)
        {
            homieContext = _homieContext;
        }

        //IF DATE IS INVALID REDIRECTS TO ALL PAGE WITHOUT CREATING THE EVENT
        public async Task CreateEventAsync(FormEventViewModel model, IdentityUser organizer)
        {
            DateTime creationDate = DateTime.Now;
            DateTime validStartDate;
            DateTime validEndDate;

            bool IsValidStartDate = DateTime
                .TryParseExact(model.Start, "yyyy-MM-dd H:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out validStartDate);

            bool IsValidEndDate = DateTime
                .TryParseExact(model.End, "yyyy-MM-dd H:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out validEndDate);

            if (IsValidStartDate && IsValidEndDate)
            {
                Event newEvent = new Event()
                {
                    Name = model.Name,
                    Description = model.Description,
                    OrganiserId = organizer.Id,
                    Organiser = organizer,
                    CreatedOn = creationDate,
                    Start = validStartDate,
                    End = validEndDate,
                    TypeId = model.TypeId,
                };

                await homieContext.Events.AddAsync(newEvent);
                await homieContext.SaveChangesAsync();
            }
        }

        //IF DATE IS INVALID REDIRECTS TO ALL PAGE WITHOUT EDITING THE EVENT
        public async Task EditEventAsync(int id, FormEventViewModel eventModel)
        {
            DateTime validStartDate;
            DateTime validEndDate;

            bool IsValidStartDate = DateTime
                .TryParseExact(eventModel.Start, "dd/MM/yyyy H:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out validStartDate);

            bool IsValidEndDate = DateTime
                .TryParseExact(eventModel.End, "dd/MM/yyyy H:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out validEndDate);

            Event? exisitingEvent = await homieContext
                .Events
                .FindAsync(id);

            if (IsValidStartDate && IsValidEndDate)
            {
                if (exisitingEvent != null)
                {
                    exisitingEvent.Name = eventModel.Name;
                    exisitingEvent.Description = eventModel.Description;
                    exisitingEvent.Start = validStartDate;
                    exisitingEvent.End = validEndDate;
                    exisitingEvent.TypeId = eventModel.TypeId;

                    await homieContext.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<AllEventViewModel>> GetAllEventsAsync()
        {
            return await homieContext
                .Events
                .Select(e => new AllEventViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString("yyyy-MM-dd H:mm"),
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TypeViewModel>> GetAllTypesAsync()
        {
            return await homieContext
                .Types
                .Select(e => new TypeViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                })
                .ToListAsync();
        }

        public async Task<DetailsViewModel?> GetDetailsOfEventViewModelAsync(int eventId)
        {
            return await homieContext
                .Events
                .Where(e => e.Id == eventId)
                .Select(e => new DetailsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Organiser = e.Organiser.UserName,
                    CreatedOn = e.CreatedOn.ToString("yyyy-MM-dd H:mm"),
                    Start = e.Start.ToString("yyyy-MM-dd H:mm"),
                    End = e.End.ToString("yyyy-MM-dd H:mm"),
                    Type = e.Type.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<FormEventViewModel> GetFilledEditEventFormAsync(int eventId)
        {
            var types = await homieContext
                .Types
                .Select(e => new TypeViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                })
                .ToListAsync();

            Event? existingEvent = await homieContext
                .Events
                .FirstOrDefaultAsync(e => e.Id == eventId);

            FormEventViewModel eventViewModel = new FormEventViewModel()
            {
                Types = types
            };

            if (existingEvent != null)
            {
                eventViewModel.Name = existingEvent.Name;
                eventViewModel.Description = existingEvent.Description;
                eventViewModel.Start = existingEvent.Start.ToString("yyyy-MM-dd H:mm");
                eventViewModel.End = existingEvent.End.ToString("yyyy-MM-dd H:mm");
                eventViewModel.TypeId = existingEvent.TypeId;
            }

            return eventViewModel;
        }

        public async Task<IEnumerable<AllEventViewModel>> GetJoinedEventsAsync(string userId)
        {
            return await homieContext
                .EventParticipants
                .Where(ep => ep.HelperId == userId)
                .Select(ep => new AllEventViewModel()
                {
                    Id = ep.EventId,
                    Name = ep.Event.Name,
                    Start = ep.Event.Start.ToString("yyyy-MM-dd H:mm"),
                    Type = ep.Event.Type.Name,
                    Organiser = ep.Event.Organiser.UserName
                })
                .ToListAsync(); 
        }

        public async Task<bool> IsAlreadyEventJoinedByUser(string userId, int eventId)
        {
            return await homieContext
                .EventParticipants
                .AnyAsync(ep => ep.HelperId == userId && ep.EventId == eventId);
        }

        public async Task JoinEventAsync(string userId, int eventId)
        {
            bool isAleradyJoined = await homieContext
                .EventParticipants
                .AnyAsync(ep => ep.HelperId == userId && ep.EventId == eventId);

            if (isAleradyJoined == false)
            {
                EventParticipant newEventParticipant = new EventParticipant()
                {
                    HelperId = userId,
                    EventId = eventId
                };

                await homieContext.EventParticipants.AddAsync(newEventParticipant);
                await homieContext.SaveChangesAsync();
            }
        }

        public async Task LeaveEventAsync(string userId, int eventId)
        {
            var eventParticipant = await homieContext
                .EventParticipants
                .FirstOrDefaultAsync(ep => ep.HelperId == userId && ep.EventId == eventId);

            if (eventParticipant != null)
            {
                homieContext.EventParticipants.Remove(eventParticipant);
                await homieContext.SaveChangesAsync();
            }
        }
    }
}
