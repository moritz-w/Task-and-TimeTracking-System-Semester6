using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskAndTimeTracking.Persistence.Context;
using TaskAndTimeTracking.Persistence.Entity;
using TaskAndTimeTracking.Persistence.Repository.Interfaces;

namespace TaskAndTimeTracking.Persistence.Repository
{
    public class WorkProgressRepository : BaseRepository<WorkProgressEntity>, IWorkProgressRepository
    {
        public WorkProgressRepository(ApplicationDatabaseContext context) : base(context)
        {
        }

        public override async Task<WorkProgressEntity> getById(int id)
        {
            return await Context.WorkProgress.Include(entity => entity.Person)
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public override async Task<List<WorkProgressEntity>> getAll()
        {
            return await Context.WorkProgress.Include(entity => entity.Person).ToListAsync();
        }
    }
}