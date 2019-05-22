using System.Threading.Tasks;
using TaskAndTimeTracking.Persistence.Entity;

namespace TaskAndTimeTracking.Persistence.Repository.Interfaces
{
    public interface ITodoRepository : IBaseRepository<TodoEntity>
    {
        Task<TodoEntity> getDetailsById(int id);
    }
}