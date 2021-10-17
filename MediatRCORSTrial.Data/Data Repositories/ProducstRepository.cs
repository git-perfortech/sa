using MediatRCORSTrial.Core.Common.Contracts;
using MediatRCORSTrial.Core.Configuration;
using MediatRCORSTrial.Core.Responses;
using MediatRCORSTrial.Data.Contracts;
using MediatRCORSTrial.Data.Entities;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Data.Data_Repositories
{
    public class ProducstRepository : Repository<Product, int>, IProducstRepository
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public ProducstRepository(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task<ResponseDTO> CreateProductAsync(Guid productId)
        {
            Product entity = new Product { Details="",  Name="productname", Price=4, StockQuantity=19};

            IsolationLevel isoLevel = IsolationLevel.ReadCommitted;
            bool entityLazyLoad = true;
            using (IUnitOfWork uow = _unitOfWorkFactory.Create(ContextKeys.MediatRCORSTrialCoreApiConfiguration, isoLevel, entityLazyLoad))
            {
                try
                {
                    uow.Begin(false);
                    Insert(entity);
                    uow.Commit();
                    return await CreateResponseWithData<ResponseDTO>.Return(true);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    throw new BusinessException(ResponseCodes.Failed, ex.Message);
                }
            }
        }

        public Task<Product> GetProductAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();

            //IsolationLevel isoLevel = IsolationLevel.ReadCommitted;
            //bool entityLazyLoad = true;
            //using (IUnitOfWork uow = _unitOfWorkFactory.Create(ContextKeys.MediatRCORSTrialCoreApiConfiguration, isoLevel, entityLazyLoad))
            //{
            //    try
            //    {
            //        uow.Begin(false);
            //        Update(entity);
            //        uow.Commit();
            //        return await CreateResponseWithData<ResponseDTO>.Return(true);
            //    }
            //    catch (Exception ex)
            //    {
            //        uow.Rollback();
            //        throw new BusinessException(ResponseCodes.Failed, ex.Message);
            //    }
            //}
        }
    }
}
