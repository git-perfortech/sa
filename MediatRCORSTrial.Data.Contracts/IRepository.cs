using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Data.Contracts
{
    public interface IRepository2<TEntity, TKey>
        where TEntity : class
    {
        TEntity GetAsync(TKey id);
        IQueryable<TEntity> GetAll(int skip, int take);
        IQueryable<TEntity> GetAll(int skip, int take, Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);
        void Delete(TEntity entity);
        Task SaveChangeAsync();
    }

    public interface IRepository<TEntity, T>
       where TEntity : class
    {
        Task<IEnumerable<TEntity>> FindAll();
        Task<IEnumerable<TEntity>> FindByCondition(Expression<Func<TEntity, bool>> expression);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteBulk(IEnumerable<TEntity> entity);
        void InsertBulk(IEnumerable<TEntity> entity);
        void UpdateBulk(IEnumerable<TEntity> entity);
        Task<TEntity> GetByKey(T key);
        /// <summary>
        /// Query Method
        /// </summary>
        /// <returns>RepositoryQueryHelper (Sorgu Yardımcı Sınıfı)</returns>
        IRepositoryQueryHelper<TEntity, T> Query();

        /// <summary>
        /// To Set Data to Table, Definition Some Helper Parameters
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null);
    }
}
