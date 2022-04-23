using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Interface;

namespace PresonelManagmentBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController :ControllerBase
    {
        private readonly IEventRepo _repository;

        public EventsController(IEventRepo repository)
        {
            _repository = repository;
        }
        //GET api/events
        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetAllEvent()
        {
            var events = _repository.GetAllEvents();
            return Ok(events);
        }
    }
}