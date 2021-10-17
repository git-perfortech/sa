using MediatR;
using MediatRCORSTrial.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public Guid ProductId { get; }
        public GetProductByIdQuery(Guid id)
        {
            ProductId = id;
        }

    }
}
