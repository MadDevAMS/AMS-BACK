using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using FluentValidation;

namespace AMS.Application.UseCases.Users.Command.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommnad>
    {
        public UpdateUserValidator()
        {

            RuleFor(x => x.FirstName)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.LastName)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage(MessageValidator.NOT_NULL)
               .MinimumLength(8).WithMessage(MessageValidator.PASSWORD_LEGHT)
               .Matches(@"[A-Za-z0-9]*[@#$%^&+=][A-Za-z0-9]*").WithMessage(MessageValidator.PASSWORD_SPECIAL_CHARACTER)
               .When(x => x.UpdatePassword);
        }
    }
}
