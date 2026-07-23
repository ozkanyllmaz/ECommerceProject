using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-postanızı girin");
            RuleFor(x => x.Email).EmailAddress().WithMessage("E-posta formatına uygun değil");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifrenizi girin");
            //RuleFor(x => x.Password).MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı");
            //RuleFor(x => x.Password).MaximumLength(20).WithMessage("Şifre en fazla 20 karakter olmalı");
            
        }
    }
}
