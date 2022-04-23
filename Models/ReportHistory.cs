using System;
using Microsoft.VisualBasic;

namespace PresonelManagmentBE.Models
{
    public class ReportHistory
    {
        public int Id { get; set; }
        public StaffUser User { get; set; }
        public DateTime DateTime { get; set; }
        public int SumOfHours { get; set; }
        public int SumOfReports { get; set; }
    }
}