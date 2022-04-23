using System.Collections;
using System.Collections.Generic;
using PresonelManagmentBE.Dtos;

namespace PresonelManagmentBE.Interface
{
    public interface IEventRepo
    {
        IEnumerable<Event> GetAllEvents();
    }
}