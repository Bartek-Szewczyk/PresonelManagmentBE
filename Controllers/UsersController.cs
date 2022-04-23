using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Interface;
using PresonelManagmentBE.Models;

namespace PresonelManagmentBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var usersItems = _repository.GetAllUsers();
            return _mapper.Map<ActionResult<IEnumerable<User>>>(usersItems);
        }

    }
}