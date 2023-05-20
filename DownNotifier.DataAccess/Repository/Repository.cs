using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DownNotifier.DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext dbContext)
        {
            _context = dbContext;
            _entities = _context.Set<TEntity>();
        }
         
        public virtual TEntity Insert(TEntity entity)
        {
            if (entity != null && entity.GetType().GetProperty("ResourceId") != null)
            {
                var propertyInfo = entity.GetType().GetProperty("ResourceId");
                propertyInfo?.SetValue(entity,
                    Convert.ChangeType(Guid.NewGuid(),
                        propertyInfo.PropertyType), null);
            }

            _entities.Add(entity);
            _context.SaveChanges();
            return entity;
        }
         
         

        public virtual TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Remove(entity);
            _context.SaveChanges();
        } 
         
        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public virtual IQueryable<TEntity> FindAll()
        {
            return _entities;
        } 
    }
}
