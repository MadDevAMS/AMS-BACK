using AMS.Application.Commons.Bases;
using FluentValidation;

namespace AMS.Application.UseCases.User.Command.UpdateUser
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
