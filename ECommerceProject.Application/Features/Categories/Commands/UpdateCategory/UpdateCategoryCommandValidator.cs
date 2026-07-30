using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommandRequest>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori isim alanı boş geçilemez");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Kategori ismi 2 karakterden az olamaz");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Kategori ismi 50 karakterden fazla olamaz");
        }
    }
}
