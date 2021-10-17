using MediatR;
using MediatRCORSTrial.Commands;
using MediatRCORSTrial.Core.Responses;
using MediatRCORSTrial.Core.Responses.Wrappers;
using MediatRCORSTrial.Data.Contracts;
using MediatRCORSTrial.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Handlers
{
    //public class CreateProductHandler : IRequestHandler<CreateProductCommand, Response<Product>>
    //public class CreateProductHandler : IHandlerWrapper<CreateProductCommand, Product>
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ResponseDTO>
    {
        private readonly IProducstRepository _productsRepository;

        public CreateProductHandler(IProducstRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        //public async Task<Response<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        //{
        //    var product = await _productsRepository.CreateProductAsync(request.ProductId);
        //    ////_mapper  to map  product dto to product response
        //    //return product;

        //    if (string.IsNullOrEmpty(request.UserId.ToString()))
        //    {
        //        return Task.FromResult( Response.Fail<Product>("Id property cannot be null") ).Result;
        //    }

        //    return Task.FromResult(
        //        Response.Success(
        //            product,
        //           "Product Created"
        //        )).Result;
        //}

        public async Task<ResponseDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.CreateProductAsync(request.ProductId);

            return product;
        }
    }
}
