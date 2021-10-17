using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text;
using System.Text.Json;
//using System.ComponentModel.DataAnnotations;

namespace MediatRCORSTrial.Core.Common.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        //Don't recommend using this in prod. Just use the AspNetCore extensions of FluentValidation
        //This is just here to allow me to demonstrate the pipelineBehaviours feature without api validation conflictd

        public static void UseFluentValidationExceptionHander(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    if (!(exception is ValidationException validationException))
                    {
                        throw exception;
                    }

                    //var errors = validationException.Errors.Select(err =>
                    //{
                    //    err.PropertyName,
                    //    err.ErrorMessage
                    //});
                    var errors = validationException.Errors;
                    var errorText = JsonSerializer.Serialize(errors);
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(errorText, Encoding.UTF8);

                });

            });
        }
    }
}
