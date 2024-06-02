namespace AMS.Application.UseCases.Group.Command.CreateGroup;

using AMS.Application.Commons.Bases;

// CreateGroupValidator.cs
using FluentValidation;

    public class CreateGroupValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupValidator()
        {
            RuleFor(x => x.Description)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY)
                .Length(1, 100).WithMessage(MessageValidator.DESCRIPTION_LENGHT);
        }
    }

// ejemplo, lo mas posible es que se divida esto.

