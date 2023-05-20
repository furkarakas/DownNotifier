using System.Linq.Expressions;

namespace DownNotifier.DataAccess.Repository
{
    public interface IRepository<TEntity>
    { 
        TEntity Insert (TEntity entity);
            
        TEntity Update(TEntity entity);

        void Delete(TEntity entity); 
          
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> FindAll(); 
    }
}
