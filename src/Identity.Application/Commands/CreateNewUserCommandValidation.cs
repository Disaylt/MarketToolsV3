using FluentValidation;
using Identity.Application.Commands;
using Identity.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands
{
    public class CreateNewUserCommandValidation : AbstractValidator<CreateNewUserCommand>
    {
        public CreateNewUserCommandValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Введите почту.")
                .EmailAddress().WithMessage("Почта введена с ошибками.")
                .MaximumLength(150).WithMessage("Максимальная длина почты 150 символов.");
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Введите логин.")
                .MinimumLength(6).WithMessage("Минимальная длина логина 6 символов.")
                .MaximumLength(150).WithMessage("Максимальная длина логина 150 символов.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Введите пароль.")
                .MinimumLength(6).WithMessage("Минимальная длина пароля 6 символов.")
                .MaximumLength(50).WithMessage("Максимальная длина пароля 50 символов.");
        }
    }
}
