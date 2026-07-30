using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.ShoppingCarts.Queries.ListItemInShoppingCart
{
    public class ListItemInShoppingCartQueryRequest : IRequest<CustomResponseDto<List<ListItemInShoppingCartQueryResponse>>>, ISecuredRequest
    {
        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
