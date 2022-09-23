using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PresonelManagmentBE.Dtos;

namespace PresonelManagmentBE.Models
{
    public class UserEvents
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Event Event { get; set; }
        public bool Approved { get; set; }
    }
}