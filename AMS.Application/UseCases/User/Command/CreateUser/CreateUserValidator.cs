﻿using AMS.Application.Commons.Bases;
using FluentValidation;

namespace AMS.Application.UseCases.User.Command.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.LastName)
                .NotNull().WithMessage(MessageValidator.NOT_NULL)
                .NotEmpty().WithMessage(MessageValidator.NOT_EMPTY);

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(MessageValidator.BAD_EMAIL);

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage(MessageValidator.NOT_NULL)
               .MinimumLength(8).WithMessage(MessageValidator.PASSWORD_LEGHT)
               .Matches(@"[A-Za-z0-9]*[@#$%^&+=][A-Za-z0-9]*").WithMessage(MessageValidator.PASSWORD_SPECIAL_CHARACTER);
        }
    }
}