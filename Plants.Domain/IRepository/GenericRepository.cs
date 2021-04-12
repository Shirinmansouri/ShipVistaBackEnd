using Microsoft.EntityFrameworkCore;
using Plants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Plants.Domain.IRepository
{
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
   where TEntity : BaseEntity, new()
   where TContext : DbContext
    {
        protected TContext Context;

        public GenericRepository(TContext context)
        {
            Context = context;
        }

 
        public Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
                   => Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        public virtual async Task Add(TEntity entity)
        {
            // await Context.AddAsync(entity);
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }
        public virtual Task Update(TEntity entity)
        {
            // In case AsNoTracking is used
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }
        public virtual Task Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Context.SaveChangesAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }
        public virtual async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }
        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }
         

    }
}
