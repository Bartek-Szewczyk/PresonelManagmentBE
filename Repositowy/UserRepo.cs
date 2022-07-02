using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepo(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IEnumerable<User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            var dbCategory = _context.Categories.ToList();
            var apiUser = new List<User>();
            foreach (var user in users.Select(singleUser => new User
            {
                Id = singleUser.Id,
                FirstName = singleUser.FirstName,
                LastName = singleUser.LastName,
                email = singleUser.Email,
                phone = singleUser.PhoneNumber,
                Category = dbCategory.FirstOrDefault(c => c.Id == singleUser.Category.Id)
            }))
            {
                apiUser.AddRange(new[] { user });
            }
            return apiUser ;
        }

        public User GetUserById(string id)
        {
            var userDb = _context.Users.FirstOrDefault(u => u.Id == id);
            var dbCategory = _context.Categories.ToList();
            if (userDb == null) return null;
            var user = new User
            {
                Id = userDb.Id,
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                email = userDb.Email,
                phone = userDb.PhoneNumber,
                Category = dbCategory.FirstOrDefault(c => c.Id == userDb.Category.Id),
                HourlyRate = userDb.HourlyRate,
            };
            return user;

        }

        public void UpdateUser(User user)
        {
            var userDb = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            var categoryDb = _context.Categories.ToList();
            if (userDb == null) return;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.email;
            userDb.PhoneNumber = user.phone;
            userDb.Category = categoryDb.FirstOrDefault(c => c.Id == user.Category.Id);
            userDb.HourlyRate = user.HourlyRate;
            _context.SaveChanges();
        }

        public async Task<IdentityResult> AddUser(User user)
        {
            const string userPWD = "Password@1234";
            var categoryDb = _context.Categories.ToList();
            string userName = $"{user.FirstName}{user.LastName}";
            ApplicationUser newUser = new ()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = userName,
                Email = user.email,
                PhoneNumber = user.phone,
                SecurityStamp = Guid.NewGuid().ToString(),
                Category = categoryDb.FirstOrDefault(c => c.Id == user.Category.Id),
                HourlyRate = user.HourlyRate,
            };
            var createPowerUser = await _userManager.CreateAsync(newUser, userPWD);
            if (createPowerUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User); 
            }

            return createPowerUser;
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