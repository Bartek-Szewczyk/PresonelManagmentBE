using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}