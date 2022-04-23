using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PresonelManagmentBE.Models
{
    public class StaffUser
    {
        public int Id { get; set; }
        
        public ApplicationUser User { get; set; }
        public Category Category { get; set; }
        public byte CategoryId { get; set; }
        public int HourlyRate { get; set; }
       // public ReportHistory ReportHistory { get; set; }
    }
}