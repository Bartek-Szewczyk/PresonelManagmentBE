using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PresonelManagmentBE.Data;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Interface;
using PresonelManagmentBE.Models;
using Event = PresonelManagmentBE.Dtos.Event;

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
                    BackgroundColor = singleEvent.BackgroundColor,
                    Staff = getStaffList(singleEvent.Id)
                };
                apiEvents.AddRange(new []{newEvent});
            }

            return apiEvents;
        }

        public Event GetEventById(int id)
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
                    BackgroundColor = dbEvent.BackgroundColor,
                    Staff = getStaffList(id)
                };

                return apiEvent;
            
        }
        
        private List<StaffUser> getStaffList(int eventId)
        {
            var dbUserEvents = _context.UserEvents.ToList();
            var dbUsers = _context.Users.ToList();
            
            var staffListId = dbUserEvents.FindAll(u=>u?.Event?.Id == eventId);
            var staffList = new List<StaffUser>();
            foreach(var staff in staffListId)
            {
                var user = dbUsers.FirstOrDefault(u=>u.Id == staff.User.Id);
                var staffUser = new StaffUser
                {
                    userId = user.Id,
                    name = user.FirstName,
                    surname = user.LastName,
                    approved = staff.Approved
                };
                staffList.Add(staffUser);
            }
            return staffList;
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

        public EntityEntry<Models.Event> RemoveEvent(Event rmEvent)
        {
            var dbEvent = _context.Events.FirstOrDefault(e=>e.Id==rmEvent.Id);
            return _context.Events.Remove(dbEvent);
        }

        public void UpdateEvent(Models.Event editEvent)
        {
            var dbEvent = _context.Events.FirstOrDefault(e=>e.Id==editEvent.Id);
            var dbCategory = _context.Categories.ToList();
            if (dbEvent == null) return;
            dbEvent.Title = editEvent.Title;
            dbEvent.Category = dbCategory.FirstOrDefault(c => c.Id == editEvent.Category.Id);
            dbEvent.AllDay = editEvent.AllDay;  
            dbEvent.DateStart = editEvent.DateStart;
            dbEvent.DateEnd = editEvent.DateEnd;
            dbEvent.StaffNumber = editEvent.StaffNumber;
            dbEvent.BackgroundColor = editEvent.BackgroundColor;
            _context.SaveChanges();
            
        }

        public UserEvents AddUserToEvent(string userName, int eventId)
        {
            var dbEvent = _context.Events.FirstOrDefault(e=>e.Id==eventId);
            var dbUser = _context.Users.FirstOrDefault(u=>u.UserName==userName);
            var userEv = new UserEvents
            {
                User = dbUser,
                Event = dbEvent,
                Approved = false
            };
             _context.UserEvents.Add(userEv);
             return userEv;
        }
        public void UpdateUserToEvent(UserToEvent userToEvent)
        {
            var dbUserEvent = _context.UserEvents.FirstOrDefault(e=>e.Event.Id==userToEvent.eventId && e.User.Id==userToEvent.userId);
            if (dbUserEvent != null) dbUserEvent.Approved = userToEvent.approved;
            _context.SaveChanges();
        }

        public void DeleteUserToEvent(string userName, int eventId)
        {
            var dbUser = _context.Users.FirstOrDefault(u=>u.UserName==userName);
            var dbUserEvent = _context.UserEvents.FirstOrDefault(e=>e.Event.Id==eventId && e.User.Id==dbUser.Id);
            if (dbUserEvent != null) _context.UserEvents.Remove(dbUserEvent);
            _context.SaveChanges();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}