using MediatR;
using MediatRCORSTrial.Data.Entities;
using System.Collections.Generic;

namespace MediatRCORSTrial.Queries
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {

    }
}
