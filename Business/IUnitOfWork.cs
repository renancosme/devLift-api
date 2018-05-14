using Business.Repositories;
using System;

namespace Business
{
    public interface IUnitOfWork : IDisposable
    {
        IEventRepository Events { get; }
        int Complete();
    }
}
