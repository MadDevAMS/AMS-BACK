using AMS.Application.Commons.Utils;
using FluentValidation;

namespace AMS.Application.UseCases.Entidades.Command.CreateEntidad
{
    public class CreateEntidadValidator : AbstractValidator<CreateEntidadCommand>
    {
        public CreateEntidadValidator()
        {
            RuleFor(x => x.Nombre)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.RazonSocial)
                .NotNull().WithMessage(MessageValidator.NOT_NULL) 
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.RUC)
                .MinimumLength(11).WithMessage(MessageValidator.RUC_LEGHT)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(MessageValidator.BAD_EMAIL)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.FirstName)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.LastName)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.UserEmail)
                .EmailAddress().WithMessage(MessageValidator.BAD_EMAIL)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);


            RuleFor(x => x.Telefono)
                .MinimumLength(9).WithMessage(MessageValidator.TELEFONO_LEGHT_MIN)
                .MaximumLength(10).WithMessage(MessageValidator.TELEFONO_LEGHT_MAX)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage(MessageValidator.NOT_NULL)
               .MinimumLength(8).WithMessage(MessageValidator.PASSWORD_LEGHT)
               .Matches(@"[A-Za-z0-9]*[@#$%^&+=][A-Za-z0-9]*").WithMessage(MessageValidator.PASSWORD_SPECIAL_CHARACTER);

            RuleFor(x => x.ConfirmPassword)
               .NotEmpty().WithMessage(MessageValidator.NOT_NULL)
               .MinimumLength(8).WithMessage(MessageValidator.PASSWORD_LEGHT)
               .Matches(@"[A-Za-z0-9]*[@#$%^&+=][A-Za-z0-9]*").WithMessage(MessageValidator.PASSWORD_SPECIAL_CHARACTER);
        }
    }
}
