using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PresonelManagmentBE.Data;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Interface;

namespace PresonelManagmentBE.Repositowy
{
    public class EventRepo :IEventRepo
    {
        private readonly ApplicationDbContext _context;

        public EventRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Event> GetAllEvents()
        {
            var dbEvents = _context.Events.ToList();
            var dbCategory = _context.Categories.ToList();
            var apiEvents = new List<Event>();
            foreach (var singleEvent in dbEvents)
            {
                var newEvent = new Event
                {
                    Id = singleEvent.Id,
                    Title = singleEvent.Title,
                    Category = singleEvent.Category,
                    AllDay = singleEvent.AllDay,
                    DateStart = singleEvent.DateStart,
                    DateEnd = singleEvent.DateEnd,
                    StaffNumber = singleEvent.StaffNumber,
                    BackgroundColor = singleEvent.BackgroundColor
                };
                apiEvents.AddRange(new []{newEvent});
            }

            return apiEvents;
        }

        public Models.Event GetEventById(int id)
        {
            var dbCategory = _context.Categories.ToList();
            var dbEvent = _context.Events.FirstOrDefault(e=>e.Id==id);

            var apiEvent = new Event
                {
                    Id = dbEvent.Id,
                    Title = dbEvent.Title,
                    Category = dbCategory.FirstOrDefault(c=>c.Id == dbEvent.Category.Id),
                    AllDay = dbEvent.AllDay,
                    DateStart = dbEvent.DateStart,
                    DateEnd = dbEvent.DateEnd,
                    StaffNumber = dbEvent.StaffNumber,
                    BackgroundColor = dbEvent.BackgroundColor
                };

                return dbEvent;
            
        }

        public EntityEntry<Models.Event> AddEvent(Event addEvent)
        {
            var dbCategory = _context.Categories.ToList();
            var dtosEvent = addEvent;
            var dbEvent = new Models.Event
            {
                Title = dtosEvent.Title,
                Category = dbCategory.FirstOrDefault(c=>c.Id == dtosEvent.Category.Id),
                AllDay = dtosEvent.AllDay,
                DateStart = dtosEvent.DateStart,
                DateEnd = dtosEvent.DateEnd,
                StaffNumber = dtosEvent.StaffNumber,
                BackgroundColor = dtosEvent.BackgroundColor
            };
            return _context.Events.Add(dbEvent);
        }

        public EntityEntry<Models.Event> RemoveEvent(Models.Event rmEvent)
        {
            return _context.Events.Remove(rmEvent);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}