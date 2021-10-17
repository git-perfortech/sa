using MediatRCORSTrial.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Api.Filters
{
    //this validation is what will happen before we get into the controller
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //before controller
            //before the controller is being hit we wanna validate the model state
            if (!context.ModelState.IsValid)
            {
                //First get all the errors in the model state
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        var model = new ErrorModel
                        {
                            FieldName = error.Key,
                            Message = subError
                        };
                        errorResponse.Errors.Add(model);
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();

           //after controller

        }
    }
}
