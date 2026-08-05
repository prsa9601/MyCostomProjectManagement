using BackEnd.Data.DB;
using BackEnd.Shared.DataShared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace BackEnd.Shared.CoreShared.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly Context Context;
        public BaseRepository(Context context)
        {
            Context = context;
        }

        public virtual async Task<TEntity?> GetAsync(Guid id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(id)); ;
        }


        public async Task<List<TEntity>?> GetListByFilterAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().Where(expression).AsTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity?> GetByFilterWithIncludsAsync(
       Expression<Func<TEntity, bool>> expression,
       params string[] includs)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsTracking();

            // اعمال Include‌ها (اگر آرایه خالی نباشد)
            if (includs != null && includs.Length > 0)
            {
                foreach (var include in includs)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity?> GetTracking(Guid id)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<TEntity?> GetTracking(Guid id, params string[] includs)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsTracking();

            // اعمال Include‌ها (اگر آرایه خالی نباشد)
            if (includs != null && includs.Length > 0)
            {
                foreach (var include in includs)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(i => i.Id == id);
        }

 
        public async Task<List<TEntity>?> GetListTrackingAsync()
        {
            return await Context.Set<TEntity>().AsTracking().Select(i => i).ToListAsync();

        }
        public async Task<TEntity?> GetTrackingWithString(string id)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));

        }
        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        void IBaseRepository<TEntity>.Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public async Task AddRange(ICollection<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }
        public void Update(TEntity entity)
        {
            Context.Update(entity);
        }
        public async Task<int> Save()
        {
            return await Context.SaveChangesAsync();
        }
        public int SaveChange()
        {
            return Context.SaveChanges();
        }
        public async Task<int> SaveChangeAsync()
        {
            //try
            //{
            //    return await Context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    // هندل کردن concurrency exception
            //    foreach (var entry in ex.Entries)
            //    {
            //        if (entry.Entity is UserOtp)
            //        {
            //            var databaseValues = await entry.GetDatabaseValuesAsync();
            //            if (databaseValues != null)
            //            {
            //                entry.OriginalValues.SetValues(databaseValues);
            //            }
            //        }
            //    }
            //    throw;
            //}
            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveChangeAsync(TEntity entity)
        {
            //Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;

            return await Context.SaveChangesAsync();


            //Context.Entry(entity).State = EntityState.Modified;
            //return await Context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().AnyAsync(expression);
        }
        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Any(expression);
        }

        public TEntity? Get(long id)
        {
            return Context.Set<TEntity>().FirstOrDefault(t => t.Id.Equals(id)); ;
        }

        public async Task<bool> Delete(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var Entity = await Context.Set<TEntity>().AsTracking().Where(expression).ToListAsync();
                Context.Set<TEntity>().RemoveRange(Entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteOneEntity(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var Entity = await Context.Set<TEntity>().AsTracking().Where(expression).FirstOrDefaultAsync();
                Context.Set<TEntity>().Remove(Entity!);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                var Entity = Context.Set<TEntity>().Remove(entity);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}