using Business;
using Business.Repositories;
using DevLiftApp.Persistence.Repositories;

namespace DevLiftApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DevLiftContext _context;

        public UnitOfWork(DevLiftContext context)
        {
            _context = context;
            Events = new EventRepository(_context);
        }

        public IEventRepository Events { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
