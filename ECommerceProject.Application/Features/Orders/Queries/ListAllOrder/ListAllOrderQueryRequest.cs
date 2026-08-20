using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Queries.ListAllOrder
{
    public class ListAllOrderQueryRequest : IRequest<CustomResponseDto<PaginationResult<ListAllOrderQueryResponse>>>, ISecuredRequest
    {
        public PaginationParameter paginationParameter { get; set; } = null!;
        public string[] Roles => ["Admin", "Manager"];
    }
}
