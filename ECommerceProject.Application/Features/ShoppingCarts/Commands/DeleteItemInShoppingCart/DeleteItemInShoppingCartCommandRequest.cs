using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.ShoppingCarts.Commands.DeleteItemInShoppingCart
{
    public class DeleteItemInShoppingCartCommandRequest : IRequest<CustomResponseDto>, ISecuredRequest
    {
        public string ProductId { get; set; } = null!;
        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
