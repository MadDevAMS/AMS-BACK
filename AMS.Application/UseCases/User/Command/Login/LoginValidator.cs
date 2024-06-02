using AMS.Application.Commons.Utils;
using FluentValidation;

namespace AMS.Application.UseCases.User.Command.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(MessageValidator.BAD_EMAIL);

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage(MessageValidator.NOT_NULL);
        }
    }
}
