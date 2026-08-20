using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.ShoppingCarts.Commands.AddItemToShoppingCart
{
    public class AddItemToShoppingCartCommandRequest : IRequest<CustomResponseDto>
    {
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; } = 0!;
    }
}
