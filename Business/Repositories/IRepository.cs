using System.Collections.Generic;

namespace Business.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int GetTotal();

        TEntity Get(int id);

        void Add(TEntity entity);

        void Remove(TEntity entity);
    }
}
