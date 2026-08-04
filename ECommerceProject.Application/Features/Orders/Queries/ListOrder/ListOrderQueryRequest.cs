using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Queries.ListOrder
{
    public class ListOrderQueryRequest : IRequest<CustomResponseDto<List<ListOrderQueryResponse>>>, ISecuredRequest
    {
        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
