using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Net.Http.Headers;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Interface;

namespace PresonelManagmentBE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class EventsController :ControllerBase
    {
        private readonly IEventRepo _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;

        public EventsController(IEventRepo repository,IMapper mapper,IUserRepo userRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepo = userRepo;
        }
        
        //GET api/events
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult<IEnumerable<Event>> GetAllEvent()
        {
            var username = User.Identity.Name;
            var userCategoryId = _userRepo.GetUserCategoryByName(username);
            var events = _repository.GetAllEvents();
            if (userCategoryId != 4)
            {
                events = events.Where(e => e.Category.Id == userCategoryId).ToArray();
            }
            
            return Ok(events);
        }
        
        //GET api/events/{id}
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}", Name = "GetEventById")]
        public ActionResult<Event> GetEventById(int id)
        {
            var singleEvent = _repository.GetEventById(id);
            if (singleEvent == null)
            {
                return NotFound();
            }
            return Ok(singleEvent);
        }
        
        //POST api/events
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public Event AddEvent(Event addEvent)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _repository.AddEvent(addEvent);
            _repository.Save();
            
            return addEvent;
        }
        // PUT /api/events/{id}
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public ActionResult EditEvent(int id, Models.Event editEvent)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _repository.UpdateEvent(editEvent);
            _repository.Save();
            return Ok();
        }
        
        //DELETE api/events/{id}
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id:int}")]
        public OkResult DeleteEvent(int id)
        {
            var rmEvent = _repository.GetEventById(id);
             _repository.RemoveEvent(rmEvent);
             _repository.Save();
             return Ok();
        }
        
    }
}