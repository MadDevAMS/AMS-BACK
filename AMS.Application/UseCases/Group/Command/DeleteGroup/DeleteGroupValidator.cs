using AMS.Application.Commons.Bases;
using FluentValidation;


namespace AMS.Application.UseCases.Group.Command.DeleteGruop
{
    public class DeleteGroupValidator : AbstractValidator<DeleteGroupCommand>
    {
        public DeleteGroupValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);
        }
    }
}
