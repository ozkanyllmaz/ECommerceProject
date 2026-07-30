using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.ShoppingCarts.Commands.ClearShoppingCart
{
    public class ClearShoppingCartCommandRequest : IRequest<CustomResponseDto>, ISecuredRequest
    {
        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
