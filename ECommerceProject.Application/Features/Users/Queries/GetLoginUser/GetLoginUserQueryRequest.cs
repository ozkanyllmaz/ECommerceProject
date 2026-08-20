using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Users.Queries.GetLoginUser
{
    public class GetLoginUserQueryRequest : IRequest<CustomResponseDto<GetLoginUserQueryResponse>>, ISecuredRequest
    {

        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
