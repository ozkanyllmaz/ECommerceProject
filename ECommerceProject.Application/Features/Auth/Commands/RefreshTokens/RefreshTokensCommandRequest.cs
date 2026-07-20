using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Auth.Commands.RefreshTokens
{
    public class RefreshTokensCommandRequest : IRequest<CustomResponseDto<RefreshTokensCommandResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
