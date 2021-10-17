using MediatR;
using MediatRCORSTrial.Core.Responses;
using MediatRCORSTrial.Core.Responses.Wrappers;
using MediatRCORSTrial.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Commands
{
    //public class CreateProductCommand : BaseRequest, IRequest<Response<Product>>
    //public class CreateProductCommand : BaseRequest, IRequestWrapper<Product>
    public class CreateProductCommand : BaseRequest, IRequest<ResponseDTO>
    {
        public Guid ProductId { get; set; }
    }
}
