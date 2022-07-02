using Microsoft.AspNetCore.Identity;

namespace PresonelManagmentBE.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Category Category { get; set; }
        public byte CategoryId { get; set; }
    }
}