using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskAndTimeTracking.Persistence.Context;
using TaskAndTimeTracking.Persistence.Entity;
using TaskAndTimeTracking.Persistence.Repository.Interfaces;

namespace TaskAndTimeTracking.Persistence.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected ApplicationDatabaseContext Context;

        public BaseRepository(ApplicationDatabaseContext context)
        {
            Context = context;
        }


        public virtual async Task<T> getById(int id)
        {
            return await Context.FindAsync<T>(id);
        }

        public virtual async Task<List<T>> getAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task update(T entity)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<T> add(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
    }
}