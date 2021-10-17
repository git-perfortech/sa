using MediatR;
using MediatRCORSTrial.Commands;
using MediatRCORSTrial.Core.Responses;
using MediatRCORSTrial.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediatR)
        {
            _mediator = mediatR;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            var product = new GetProductByIdQuery(productId);
            var result = await _mediator.Send(product);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }


        [HttpPost]
        //public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        public async Task<ResponseDTO> CreateProduct([FromBody] CreateProductCommand command)
        {
            if (!ModelState.IsValid)
            {

            }

            var result = await _mediator.Send(command);

            //return CreatedAtAction("GetProduct",  new { productId = result.Data.Id}, result);

            return result;
        }

    }
}
