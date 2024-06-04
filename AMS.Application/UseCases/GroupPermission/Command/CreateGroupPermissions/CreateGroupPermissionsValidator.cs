using AMS.Application.Commons.Utils;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.UseCases.GroupPermission.Command.CreateGroupPermissions
{
    public class CreateGroupPermissionsValidator : AbstractValidator<CreateGroupPermissionsCommand>
    {
        public CreateGroupPermissionsValidator()
        {
            RuleFor(x => x.Groups)
            .NotNull().WithMessage(MessageValidator.NOT_NULL)
            .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Permissions)
            .NotNull().WithMessage(MessageValidator.NOT_NULL)
            .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

        }
    }
}
