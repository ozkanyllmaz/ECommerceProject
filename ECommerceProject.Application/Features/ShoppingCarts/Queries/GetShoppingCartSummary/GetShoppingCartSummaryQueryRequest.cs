using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.ShoppingCarts.Queries.GetShoppingCartSummary
{
    public class GetShoppingCartSummaryQueryRequest : IRequest<CustomResponseDto<GetShoppingCartSummaryQueryResponse>>, ISecuredRequest
    {
        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
