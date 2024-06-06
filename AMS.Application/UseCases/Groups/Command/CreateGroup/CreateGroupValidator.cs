using AMS.Application.Commons.Utils;
using FluentValidation;

namespace AMS.Application.UseCases.Groups.Command.CreateGroup
{
    public class CreateGroupValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupValidator() 
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Description)
                .NotNull().WithMessage(MessageValidator.NOT_NULL);

            RuleFor(x => x.Permissions)
                .NotNull().WithMessage(MessageValidator.NOT_NULL);

            RuleFor(x => x.Users)
                .NotNull().WithMessage(MessageValidator.NOT_NULL);
        }
    }
}
