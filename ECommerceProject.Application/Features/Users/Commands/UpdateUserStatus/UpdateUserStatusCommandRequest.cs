using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Users.Commands.UpdateUserStatus
{
    public class UpdateUserStatusCommandRequest : IRequest<CustomResponseDto<UpdateUserStatusCommandResponse>>, ISecuredRequest
    {
        public string? userId { get; set; }
        public bool status { get; set; }
        public string[] Roles => ["Admin"];
    }
}
