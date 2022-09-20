using System;
using PresonelManagmentBE.Dtos;

namespace PresonelManagmentBE.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool AllDay { get; set; }
        public Category Category { get; set; }
        public int StaffNumber { get; set; }
        public string BackgroundColor { get; set; }
    }
}