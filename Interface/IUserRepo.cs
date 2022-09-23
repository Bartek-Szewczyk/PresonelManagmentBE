using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Models;

namespace PresonelManagmentBE.Interface
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(string id);
        void UpdateUser(User user);
        Task<IdentityResult> AddUser(User user);
        void DeleteUser(string id);
        int GetUserCategoryByName(string name);
    }
}