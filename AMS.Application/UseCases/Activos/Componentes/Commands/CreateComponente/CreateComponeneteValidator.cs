using AMS.Application.Commons.Utils;
using FluentValidation;

namespace AMS.Application.UseCases.Activos.Componentes.Commands.CreateComponente
{
    internal class CreateComponeneteValidator : AbstractValidator<CreateComponenteCommand>
    {
        public CreateComponeneteValidator()
        {
            RuleFor(x => x.Description)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Name)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Potencia)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Velocidad)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

        }
    }
}
