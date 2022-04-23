using System.Collections.Generic;
using PresonelManagmentBE.Dtos;
using PresonelManagmentBE.Models;

namespace PresonelManagmentBE.Interface
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAllUsers();
    }
}