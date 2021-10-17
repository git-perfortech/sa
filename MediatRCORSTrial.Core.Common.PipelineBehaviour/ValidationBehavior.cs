using FluentValidation;
using MediatR;
using MediatRCORSTrial.Commands;
using MediatRCORSTrial.Core.Responses;
using MediatRCORSTrial.Queries;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Core.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        //private readonly field of type IEnumerable of type of IValidator of type t request
        //to essentially give me every validator in my DI container for this specific request
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly HttpContext httpContext;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IHttpContextAccessor accessor)
        {
            _validators = validators;//all validators for this specific request
            httpContext = accessor.HttpContext;
        }

        //public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //pre
            var context = new ValidationContext(request);
            var failures = _validators
                .Select(x => x.Validate(request))
                .SelectMany(x=>x.Errors)
                .Where(x => x != null)
                .ToList();


            if (failures.Any())
            {
                throw new FluentValidation.ValidationException(failures);
            }


            //var userId = httpContext.User.Claims
            //    .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;

            if (request is BaseRequest br)
            {
                br.UserId = 12345;
            }

            //return next();
            var result = await next();

            //post
            //if (result is Response<Product> productResponse)
            //{
            //    productResponse.Data.Id += 2525;

            //}

            return result;
        }
    }
}
