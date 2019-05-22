using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskAndTimeTracking.Persistence.Context;
using TaskAndTimeTracking.Persistence.Entity;
using TaskAndTimeTracking.Persistence.Repository.Interfaces;

namespace TaskAndTimeTracking.Persistence.Repository
{
    public class TodoRepository : BaseRepository<TodoEntity>, ITodoRepository
    {
        public TodoRepository(ApplicationDatabaseContext context) : base(context)
        {
            
        }

        public async Task<TodoEntity> getDetailsById(int id)
        {
            return await Context.Todos.Include(o => o.TodoUserAssignments)
                .Include(o => o.Project)
                .Include(o => o.Project.ProjectUserAssignments)
                .Include(o => o.WorkProgressEntities)
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }
    }
}