using PresonelManagmentBE.Models;

namespace PresonelManagmentBE.Dtos
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Category Category { get; set; }
        public int HourlyRate { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}