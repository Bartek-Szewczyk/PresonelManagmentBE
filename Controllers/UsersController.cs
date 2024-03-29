﻿using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Interface;
using PresonelManagmentBE.Models;

namespace PresonelManagmentBE.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IUserRepo _repository;

        public UsersController(IUserRepo repository)
        {
            _repository = repository;
        }
        //GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var usersItems = _repository.GetAllUsers();
            return Ok(usersItems);
        }
        //GET api/users/{id}
        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult GetUserById(string id)
        {
            var user = _repository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        
        //POST api/users
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            var result = _repository.AddUser(user);
            return !result.Result.Succeeded ? StatusCode(StatusCodes.Status500InternalServerError, new Response {Status = "Error", Message = "User creation failed"}) : Ok(new Response{Status = "Success", Message = "User created successfully"});
        }
        
        //PUT api/users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(string id, User user)
        {
            
            var userFromRepo = _repository.GetUserById(id);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            _repository.UpdateUser(user);
            return Ok();
        }
        
        //DELETE api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(string id)
        {
            var userFromRepo = _repository.GetUserById(id);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteUser(id);
            return Ok();
        }

    }
}