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
                var user = new User();
                user.Id = singleUser.Id;
                user.FirstName = singleUser.FirstName;
                user.LastName = singleUser.LastName;
                apiUser.AddRange(new[] { user });

            }
            return apiUser ;
        }
    }
}