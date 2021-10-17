using MediatR;
using MediatRCORSTrial.Data.Contracts;
using MediatRCORSTrial.Data.Entities;
using MediatRCORSTrial.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Handlers
{
    public class GetAllProductsHandlers : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IProducstRepository _productsRepository;

        public GetAllProductsHandlers(IProducstRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public GetAllProductsHandlers()
        {

        }
        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productsRepository.GetProductsAsync();
            //Mapper kullanılacak dto dan response döndürülecek
            return products.ToList();
        }
    }
}
