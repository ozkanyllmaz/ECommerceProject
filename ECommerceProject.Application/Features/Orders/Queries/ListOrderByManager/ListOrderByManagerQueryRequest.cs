using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Queries.ListOrderByManager
{
    public class ListOrderByManagerQueryRequest : IRequest<CustomResponseDto<List<ListOrderByManagerQueryResponse>>>, ISecuredRequest
    {
        public string[] Roles => ["Manager"];
    }
}
