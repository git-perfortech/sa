using MediatR;
using MediatRCORSTrial.Data.Contracts;
using MediatRCORSTrial.Data.Entities;
using MediatRCORSTrial.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProducstRepository _productsRepository;

        public GetProductByIdHandler(IProducstRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetProductAsync(request.ProductId);

            return product == null ? null : product;//_mapper map dto to product response 
        }
    }
}
