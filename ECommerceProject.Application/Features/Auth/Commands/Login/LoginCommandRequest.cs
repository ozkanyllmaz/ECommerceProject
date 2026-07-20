using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Auth.Commands.Login
{
    public class LoginCommandRequest : IRequest<CustomResponseDto<LoginCommandResponse>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
