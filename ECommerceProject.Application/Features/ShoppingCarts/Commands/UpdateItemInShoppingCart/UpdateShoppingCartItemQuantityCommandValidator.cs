using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.ShoppingCarts.Commands.UpdateItemInShoppingCart
{
    public class UpdateShoppingCartItemQuantityCommandValidator : AbstractValidator<UpdateShoppingCartItemQuantityCommandRequest>
    {
        public UpdateShoppingCartItemQuantityCommandValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Adet değeri 0 dan büyük olmalı");
        }
    }
}
