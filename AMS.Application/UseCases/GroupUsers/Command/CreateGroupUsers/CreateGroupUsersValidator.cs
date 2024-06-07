using AMS.Application.Commons.Utils;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.UseCases.GroupUsers.Command.CreateGroupUsers
{
    public class CreateGroupUsersValidator : AbstractValidator<CreateGroupUsersCommand>
    {
        public CreateGroupUsersValidator() 
        {
            RuleFor(x => x.GroupId)
            .NotNull().WithMessage(MessageValidator.NOT_NULL)
            .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.UserId)
           .NotNull().WithMessage(MessageValidator.NOT_NULL)
           .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);


        }

    }
}
