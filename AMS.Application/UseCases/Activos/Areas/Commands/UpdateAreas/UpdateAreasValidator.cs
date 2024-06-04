using AMS.Application.Commons.Utils;
using FluentValidation;

namespace AMS.Application.UseCases.Activos.Areas.Commands.UpdateAreas
{
    public class UpdateAreasValidator : AbstractValidator<UpdateAreasCommand>
    {
        public UpdateAreasValidator()
        {
            RuleFor(x => x.Description)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Name)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);
        }
    }
}
