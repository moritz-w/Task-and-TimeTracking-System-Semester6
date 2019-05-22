using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskAndTimeTracking.Persistence.Context;
using TaskAndTimeTracking.Persistence.Entity;
using TaskAndTimeTracking.Persistence.Repository.Interfaces;

namespace TaskAndTimeTracking.Persistence.Repository
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDatabaseContext context) : base(context)
        {
        }

        public async Task<UserEntity> getByEmail(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.EmailAddress.Equals(email));
        }
    }
}