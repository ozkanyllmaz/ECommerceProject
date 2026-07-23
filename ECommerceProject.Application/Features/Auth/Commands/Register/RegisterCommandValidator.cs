using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Lütfen isminizi girin");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Lütfen soyisminizi girin");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen e-posta adresinizi girin")
                .EmailAddress().WithMessage("E-postanız mail formatına uygun değil");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Lütfen şifre giriniz")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı")
                .MaximumLength(20).WithMessage("Şifre en fazla 20 karakter olmalı")
                .Matches("[A-Z]").WithMessage("Şifreniz en az bir büyük harf içermeli")
                .Matches("[a-z]").WithMessage("Şifreniz en az bir küçük harf içermeli")
                .Matches("[0-9]").WithMessage("Şifreniz en az bir rakam içermeli")
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifreniz en az bir özel karakter içermeli");
            
        }
    }
}
