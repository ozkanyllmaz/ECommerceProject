using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.ShoppingCarts.Commands.UpdateItemInShoppingCart
{
    public class UpdateShoppingCartItemQuantityCommandRequest : IRequest<CustomResponseDto>, ISecuredRequest
    {
        public int Quantity { get; set; }
        public string ProductId { get; set; } = null!;
        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
