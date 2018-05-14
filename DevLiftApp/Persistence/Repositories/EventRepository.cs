using Business.Entities;
using Business.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace DevLiftApp.Persistence.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(DevLiftContext dbContext) 
            : base(dbContext)
        {
        }

        public IEnumerable<Event> GetEvents(int pageIndex, int pageSize)
        {
            return DevLiftContext.Events
                .OrderByDescending(e => e.When)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public DevLiftContext DevLiftContext
        {
            get { return _context as DevLiftContext; }
        }
    }
}
