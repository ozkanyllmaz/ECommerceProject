using ECommerceProject.Application.Features.Products.Commands.DeleteProduct;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Commands.RestoreProduct
{
    public class RestoreProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
    {
        public RestoreProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Ürün id'si boş geçilemez");
            //RuleFor(x => x.Id.ToString()).NotEqual("00000000-0000-0000-0000-000000000000").WithMessage("Ürün id'si '00000000-0000-0000-0000-000000000000' olamaz.");
        }
    }
}
