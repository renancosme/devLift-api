using Business.Entities;
using System.Collections.Generic;

namespace Business.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        IEnumerable<Event> GetEvents(int pageIndex, int pageSize);
    }
}
