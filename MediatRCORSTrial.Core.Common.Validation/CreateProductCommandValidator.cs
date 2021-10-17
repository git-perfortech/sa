using FluentValidation;
using MediatRCORSTrial.Commands;
//TODO domain katmanına almak lazım
namespace MediatRCORSTrial.Core.Common.Validation
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        //It is possible to inject anything here in constructor to validate   
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();
        }
    }
}
