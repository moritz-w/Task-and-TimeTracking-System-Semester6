using System.Collections.Generic;
using System.Threading.Tasks;
using TaskAndTimeTracking.Persistence.Entity;

namespace TaskAndTimeTracking.Persistence.Repository.Interfaces
{
    public interface IProjectRepository : IBaseRepository<ProjectEntity>
    {
        Task<ProjectEntity> getDetailsById(int id);
    }
}