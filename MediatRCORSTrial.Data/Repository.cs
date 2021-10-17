using MediatRCORSTrial.Core.Common.Contracts;
using MediatRCORSTrial.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Data
{
    public class Repository<TEntity, T> : IRepository<TEntity, T>
        where TEntity : class
    {
        #region EntityFrameworkMember
        private DbContext DbContext { get { return (DbContext)this.DbObject.DbObjectContext; } }
        private DbSet<TEntity> DbSet { get { return this.DbContext.Set<TEntity>(); } }
        #endregion

        #region UowMember
        private IUnitOfWorkFactory UnitOfWorkFactory;
        private DbObject DbObject { get { return this.UnitOfWorkFactory.GetCurrentDbObject(); } }
        #endregion

        public object CurrentDbObject
        {
            get { return this.DbObject.DbObjectContext; }
        }

        public Repository(
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.UnitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IEnumerable<TEntity>> FindAll()
        {
            return this.DbSet;
        }

        public async Task<IEnumerable<TEntity>> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return this.DbSet.Where(expression);
        }

        public void Insert(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            this.DbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }

        public void DeleteBulk(IEnumerable<TEntity> entity)
        {
            foreach (var item in entity)
                this.DbSet.Remove(item);
        }

        public void InsertBulk(IEnumerable<TEntity> entity)
        {
            foreach (var item in entity)
                this.DbSet.Add(item);
        }

        public void UpdateBulk(IEnumerable<TEntity> entity)
        {
            foreach (var item in entity)
                this.DbSet.Update(item);
        }

        public async Task<TEntity> GetByKey(T key)
        {
            return this.DbSet.Find(key);
        }

        /// <summary>
        /// Query Method
        /// </summary>
        /// <returns>RepositoryQueryHelper</returns>
        public IRepositoryQueryHelper<TEntity, T> Query()
        {
            var repositoryGetFluentHelper =
                new RepositoryQueryHelper<TEntity, T>(this);

            return repositoryGetFluentHelper;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            IQueryable<TEntity> query = this.DbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }
    }
}
