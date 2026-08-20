using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllusersQueryRequest : IRequest<CustomResponseDto<List<GetAllusersQueryResponse>>>, ISecuredRequest
    {
        public string[] Roles => ["Admin", "Manager"];
    }
}
