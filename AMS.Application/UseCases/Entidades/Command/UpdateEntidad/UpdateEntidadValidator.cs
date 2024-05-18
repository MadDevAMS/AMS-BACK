using AMS.Application.Commons.Bases;
using FluentValidation;

namespace AMS.Application.UseCases.Entidades.Command.UpdateEntidad
{
    public class UpdateEntidadValidator : AbstractValidator<UpdateEntidadCommand>
    {
        public UpdateEntidadValidator()
        {
            RuleFor(x => x.Image)
                    .NotNull().WithMessage(MessageValidator.NOT_NULL)
                    .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Direccion)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Telefono)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(MessageValidator.BAD_EMAIL);

        }

    }
}
