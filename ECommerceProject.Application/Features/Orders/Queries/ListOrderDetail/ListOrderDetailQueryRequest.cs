using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Queries.ListOrderDetail
{
    public class ListOrderDetailQueryRequest : IRequest<CustomResponseDto<List<ListOrderDetailQueryResponse>>>, ISecuredRequest
    {
        public string OrderId { get; set; } = null!;
        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
