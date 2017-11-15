using System.Collections.Generic;

namespace Dal
{
    public interface IRepository<T>
    {
        T Insert(T model);
        bool Edit(T model);
        T Find(object id);
        IEnumerable<T> List();
        bool Delete(T model);
        bool Delete(object id);
    }
}
