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
        }
    }
}
