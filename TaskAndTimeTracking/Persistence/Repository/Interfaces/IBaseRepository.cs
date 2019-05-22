using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskAndTimeTracking.Persistence.Repository.Interfaces
{
    public interface IBaseRepository <T>
    {
        Task<T> getById(int id);

        Task<List<T>> getAll();

        Task update(T entity);
        
        Task<T> add(T entity);

        Task delete(T entity);
    }
}