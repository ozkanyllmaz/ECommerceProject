using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Queries.ListOrderByAdmin
{
    public class ListOrderByAdminQueryRequest : IRequest<CustomResponseDto<List<ListOrderByAdminQueryResponse>>>, ISecuredRequest
    {   
        public string[] Roles => ["Admin"];
    }
}
