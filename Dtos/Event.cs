using System;
using System.Collections.Generic;
using PresonelManagmentBE.Models;

namespace PresonelManagmentBE.Dtos
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
        public List<StaffUser> Staff { get; set; }
    }
}