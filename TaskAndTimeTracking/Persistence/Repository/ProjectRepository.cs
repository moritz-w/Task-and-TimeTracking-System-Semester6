using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskAndTimeTracking.Persistence.Context;
using TaskAndTimeTracking.Persistence.Entity;
using TaskAndTimeTracking.Persistence.Repository.Interfaces;

namespace TaskAndTimeTracking.Persistence.Repository
{
    public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
    {
        public ProjectRepository(ApplicationDatabaseContext context) : base(context)
        {
        }

        /**
         * Eagerly resolves the owner attribute.
         */
        public override async Task<List<ProjectEntity>> getAll()
        {
            return await Context.Projects.Include(o => o.Owner).ToListAsync();
        }

        public override async Task<ProjectEntity> getById(int id)
        {
            return await Context.Projects.Include(o => o.Owner)
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<ProjectEntity> getDetailsById(int id)
        {
            if (id < 0)
            {
                return null;
            }
            return await Context.Projects.Include(o => o.Todos)
                .Include(o => o.ProjectUserAssignments)
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

    }
}