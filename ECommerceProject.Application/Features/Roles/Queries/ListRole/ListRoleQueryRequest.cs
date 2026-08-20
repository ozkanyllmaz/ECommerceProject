using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Roles.Queries.ListRole
{
    public class ListRoleQueryRequest : IRequest<CustomResponseDto<List<ListRoleQueryResponse>>>, ISecuredRequest
    {
        public string[] Roles => ["Admin"];
    }
}
