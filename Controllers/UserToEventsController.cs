using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Interface;
using PresonelManagmentBE.Models;

namespace PresonelManagmentBE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserToEventsController : ControllerBase
    {
        private readonly IEventRepo _repository;
        
        public UserToEventsController(IEventRepo repository)
        {
            _repository = repository;
        }
        
        //POST api/userToEvent
        [HttpPost("{id:int}")]
        public ActionResult AddUserToEvent(int id)
        {
            if (User.Identity != null)
            {
                var eventUs = _repository.AddUserToEvent(User.Identity.Name, id);
                _repository.Save();
                return Ok(eventUs);
            }

            return BadRequest();
        }
        
        //Post api/userToEvent/update
        [HttpPut]
        public void UpdateUserToEvent(UserToEvent userToEvent)
        {
            _repository.UpdateUserToEvent(userToEvent);
            _repository.Save();
        }
        
        //Delete api/userToEvent/{id}
        [HttpDelete("{id:int}")]
        public void DeleteUserToEvent(int id)
        {
            if (User.Identity != null) _repository.DeleteUserToEvent(User.Identity.Name, id);
            _repository.Save();
        }

    }
}