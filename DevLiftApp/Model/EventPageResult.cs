using System;
using System.Collections.Generic;
using Business.Entities;

namespace DevLiftApp.Model
{
    public class EventPageResult
    {
        public EventPageResult(int eventCount, IEnumerable<Event> eventPageResult)
        {            
            Events = eventPageResult;
            Total = eventCount;
        }

        public IEnumerable<Event> Events { get; set; }
        
        public int Total { get; set; }
    }
}
