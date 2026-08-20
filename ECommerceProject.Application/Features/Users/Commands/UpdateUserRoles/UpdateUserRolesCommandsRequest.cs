using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Features.Users.Commands.UpdateUserStatus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Users.Commands.UpdateUserRoles
{
    public class UpdateUserRolesCommandsRequest : IRequest<CustomResponseDto<UpdateUserRolesCommandsResponse>>, ISecuredRequest
    {
        public string UserId { get; set; } = null!;
        public List<Guid> RoleIds { get; set; } = null!;
        public string[] Roles => ["Admin"];
    }
}
