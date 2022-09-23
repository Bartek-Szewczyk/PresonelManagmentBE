using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Models;
using Event = PresonelManagmentBE.Dtos.Event;

namespace PresonelManagmentBE.Interface
{
    public interface IEventRepo
    {
        IEnumerable<Event> GetAllEvents();
        Event GetEventById(int id);
        EntityEntry<Models.Event> AddEvent(Event addEvent);
        EntityEntry<Models.Event> RemoveEvent(Event rmEvent);
        void UpdateEvent(Models.Event editEvent);
        UserEvents AddUserToEvent(string userName, int eventId);
        int Save();
        void UpdateUserToEvent(UserToEvent userToEvent);
        void DeleteUserToEvent(string userName, int eventId);
    }
}