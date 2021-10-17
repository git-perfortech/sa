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
    public class RepositoryQueryHelper<TEntity, T> : IRepositoryQueryHelper<TEntity, T> where TEntity : class
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="repository">Main Repository</param>
        public RepositoryQueryHelper(IRepository<TEntity, T> repository)
        {
            this.repository = repository;
            includeProperties = new List<Expression<Func<TEntity, object>>>();
        }

        /// <summary>
        ///     To filter Operations
        /// </summary>
        /// <param name="pFilter">filter exp. : <code>.Filter(x=>x.EntityProperty != null)</code></param>
        /// <returns>Added filter functionality to RepositoryQueryHelper Class</returns>
        public IRepositoryQueryHelper<TEntity, T> Filter(Expression<Func<TEntity, bool>> pFilter)
        {
            filter = pFilter;
            return this;
        }

        /// <summary>
        ///     To Order Operations
        /// </summary>
        /// <param name="orderBy">
        ///     Order Exp.
        ///     <code>.OrderBy(x => x.OrderBy(y => y.EntityProperty).ThenBy(z => z.EntityProperty2))</code>
        /// </param>
        /// <returns>Added filter functionality to RepositoryQueryHelper Class</returns>
        public IRepositoryQueryHelper<TEntity, T> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            orderByQuerable = orderBy;
            return this;
        }

        public IRepositoryQueryHelper<TEntity, T> GroupBy(Func<IQueryable<TEntity>, IGrouping<int, GroupCountResult>> groupBy)
        {
            groupByQuerable = groupBy;
            return this;
        }

        /// <summary>
        ///     To Include Operations (Set Included Tables Data)
        /// </summary>
        /// <param name="expression">Include Exp. <code>.Include(x=>x.EntityName)</code></param>
        /// <returns>Added Include functionality to RepositoryQueryHelper Class</returns>
        public IRepositoryQueryHelper<TEntity, T> Include(Expression<Func<TEntity, object>> expression)
        {
            includeProperties.Add(expression);
            return this;
        }

        public IEnumerable<TEntity> GetPage(int pPage, int pPageSize, out int totalCount)
        {
            page = pPage;
            pageSize = pPageSize;
            totalCount = repository.Get(filter).Count();

            return repository.Get(
                filter,
                orderByQuerable, includeProperties, page, pageSize);
        }

        public async Task<IEnumerable<TEntity>> Get(bool isAsNoTracking = false)
        {
            IQueryable<TEntity> queryable = repository.Get(
                filter,
                orderByQuerable, includeProperties, page, pageSize);

            if (isAsNoTracking)
                queryable = queryable.AsNoTracking();//Look at Metin Sunbul

            return queryable;
        }

        public async Task<TEntity> GetFirst()
        {
            TEntity queryable = repository.Get(
                filter,
                orderByQuerable, includeProperties, page, pageSize).FirstOrDefault();

            return queryable;
        }

        #region Properties

        private readonly List<Expression<Func<TEntity, object>>> includeProperties;
        private readonly IRepository<TEntity, T> repository;
        private Expression<Func<TEntity, bool>> filter;

        private Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderByQuerable;

        private Func<IQueryable<TEntity>,
            IGrouping<int, GroupCountResult>> groupByQuerable;

        private int? page;
        private int? pageSize;

        #endregion
    }
}
