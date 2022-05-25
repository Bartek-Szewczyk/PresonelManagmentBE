using System.Collections.Generic;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Models;

namespace PresonelManagmentBE.Interface
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(string id);
        void UpdateUser(User user);
        User AddUser(User user);
        void DeleteUser(string id);
    }
}