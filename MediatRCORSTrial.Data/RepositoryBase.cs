using MediatRCORSTrial.Data.Contracts;
using MediatRCORSTrial.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;




namespace MediatRCORSTrial.Data
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>, IDisposable
       where TEntity : EntityBase<TKey>
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private bool _disposed;

        protected RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        #region IRepository members
        public TEntity GetAsync(TKey id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<TEntity> GetAll(int skip, int take)
        {
            return _dbSet.OrderBy(q => q.Id).Skip(skip).Take(take);
        }

        public IQueryable<TEntity> GetAll(int skip, int take, Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll(skip, take).Where(predicate);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TKey id)
        {
            var entity = GetAsync(id);

            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public Task SaveChangeAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
        #endregion

        #region IDisposable members
        ~RepositoryBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }
        #endregion
    }
}
