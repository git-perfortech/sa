using MediatRCORSTrial.Core.Responses;
using MediatRCORSTrial.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Data.Contracts
{
    public interface IProducstRepository : IRepository<Product, int>
    {
        //Queries dll kaldırılacak  ProductResponse yerine ilgili dto kullanılacak
        Task<IQueryable<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(Guid productId);
        Task<ResponseDTO> CreateProductAsync(Guid productId);
    }
}
