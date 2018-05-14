using System;

namespace Business.Entities
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }
                
        public DateTime When { get; set; }
    }
}
