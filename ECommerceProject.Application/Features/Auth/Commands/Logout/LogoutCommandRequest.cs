using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandRequest : IRequest<CustomResponseDto>, ISecuredRequest
    {
        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
