using System.Threading.Tasks;
using TaskAndTimeTracking.Persistence.Entity;

namespace TaskAndTimeTracking.Persistence.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity> getByEmail(string email);
    }
}