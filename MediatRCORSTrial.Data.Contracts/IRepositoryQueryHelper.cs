using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Data.Contracts
{
    public interface IRepositoryQueryHelper<TEntity, T> where TEntity : class
    {
        /// <summary>
        /// To filter Operations
        /// </summary>
        /// <param name="pFilter">filter exp. : <code>.Filter(x=>x.EntityProperty != null)</code></param>
        /// <returns>Added filter functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<TEntity, T> Filter(Expression<Func<TEntity, bool>> pFilter);
        /// <summary>
        /// To Order Operations
        /// </summary>
        /// <param name="orderBy">Order Exp. <code>.OrderBy(x => x.OrderBy(y => y.EntityProperty).ThenBy(z => z.EntityProperty2))</code></param>
        /// <returns>Added filter functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<TEntity, T> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        /// <summary>
        /// To GroupBy Operations
        /// </summary>
        /// <param name="groupBy">GroupBy Operations <code>.OrderBy(x => x.OrderBy(y => y.EntityProperty).ThenBy(z => z.EntityProperty2))</code></param>
        /// <returns>Added GroupBy functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<TEntity, T> GroupBy(Func<IQueryable<TEntity>, IGrouping<int, GroupCountResult>> groupBy);
        /// <summary>
        /// To Include Operations (Set Included Tables Data)
        /// </summary>
        /// <param name="expression">Include Exp. <code>.Include(x=>x.EntityName)</code></param>
        /// <returns>Added Include functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<TEntity, T> Include(Expression<Func<TEntity, object>> expression);
        /// <summary>
        /// To paging by filtered, ordered and included or not
        /// </summary>
        /// <param name="pPage">Page Number</param>
        /// <param name="pPageSize">Data number</param>
        /// <param name="totalCount">Toplam Kayıt Sayısı</param>
        /// <returns>Data List</returns>
        IEnumerable<TEntity> GetPage(
            int pPage, int pPageSize, out int totalCount);
        /// <summary>
        /// To get all data by filtered, ordered and included or not
        /// </summary>
        /// <returns>Data List</returns>
        Task<IEnumerable<TEntity>> Get(bool isAsNoTracking = false);

        /// <summary>
        /// To get all data by filtered, ordered and included or not
        /// </summary>
        /// <returns>Data List</returns>
        Task<TEntity> GetFirst();
    }
}
