using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommanValidator : AbstractValidator<DeleteProductCommandRequest>
    {
        public DeleteProductCommanValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Ürün id'si boş geçilemez");
        }
    }
}
