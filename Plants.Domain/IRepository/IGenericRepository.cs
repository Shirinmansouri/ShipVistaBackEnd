using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Plants.Domain.IRepository
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {

        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);    
        Task CommitAsync();



    }
}
