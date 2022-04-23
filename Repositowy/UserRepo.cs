using System.Collections.Generic;
using System.Linq;
using PresonelManagmentBE.Data;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Interface;
using PresonelManagmentBE.Models;

namespace PresonelManagmentBE.Repositowy
{
    public class UserRepo:IUserRepo
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            var apiUser = new List<User>();
            foreach (var singleUser in users)
            {
                var user = new User
                {
                    Id = singleUser.Id,
                    FirstName = singleUser.FirstName,
                    LastName = singleUser.LastName,
                    email = singleUser.Email,
                    phone = singleUser.PhoneNumber
                };
                apiUser.AddRange(new[] { user });

            }
            return apiUser ;
        }
    }
}