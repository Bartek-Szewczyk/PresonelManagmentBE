using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            foreach (var user in users.Select(singleUser => new User
            {
                Id = singleUser.Id,
                FirstName = singleUser.FirstName,
                LastName = singleUser.LastName,
                email = singleUser.Email,
                phone = singleUser.PhoneNumber
            }))
            {
                apiUser.AddRange(new[] { user });
            }
            return apiUser ;
        }

        public User GetUserById(string id)
        {
            var userDb = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userDb == null) return null;
            var user = new User
            {
                Id = userDb.Id,
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                email = userDb.Email,
                phone = userDb.PhoneNumber
            };
            return user;

        }

        public void UpdateUser(User user)
        {
            var userDb = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (userDb == null) return;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.email;
            userDb.PhoneNumber = user.phone;
            _context.SaveChanges();
        }

        public User AddUser(User user)
        {
            var newUser = new ApplicationUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.email,
                PhoneNumber = user.phone
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return user;
        }

        public void DeleteUser(string id)
        {
            var userDb = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userDb == null) return;
            _context.Users.Remove(userDb);
            _context.SaveChanges();
        }
    }
}