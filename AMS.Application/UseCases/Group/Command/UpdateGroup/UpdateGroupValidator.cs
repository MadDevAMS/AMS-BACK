using AMS.Application.Commons.Bases;
using FluentValidation;

namespace AMS.Application.UseCases.Group.Command.UpdateGruop
{
    public class UpdateGroupValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupValidator()
        {
            RuleFor(x => x.Name)
             .NotNull().WithMessage(MessageValidator.NOT_NULL)
             .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY)
             .Length(1, 100).WithMessage(MessageValidator.DESCRIPTION_LENGHT);
            RuleFor(x => x.Id)
             .NotNull().WithMessage(MessageValidator.NOT_NULL)
             .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);
        }
    }
}
