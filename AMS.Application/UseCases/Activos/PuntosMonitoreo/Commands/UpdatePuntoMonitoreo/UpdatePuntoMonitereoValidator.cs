using AMS.Application.Commons.Utils;
using FluentValidation;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.UpdatePuntoMonitoreo
{
    internal class UpdatePuntoMonitereoValidator : AbstractValidator<UpdatePuntoMonitoreoCommand>
    {
        public UpdatePuntoMonitereoValidator()
        {
            RuleFor(x => x.IdPuntoMonitoreo)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Description)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Detail)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.DireccionMedicion)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);


            RuleFor(x => x.DatosMedicion)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.AnguloDireccion)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

        }
    }
}
