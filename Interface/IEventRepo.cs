using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PresonelManagmentBE.Dtos;

namespace PresonelManagmentBE.Interface
{
    public interface IEventRepo
    {
        IEnumerable<Event> GetAllEvents();
        Event GetEventById(int id);
        EntityEntry<Models.Event> AddEvent(Event addEvent);
        EntityEntry<Models.Event> RemoveEvent(Event rmEvent);
        void UpdateEvent(Models.Event editEvent);
        int Save();
    }
}