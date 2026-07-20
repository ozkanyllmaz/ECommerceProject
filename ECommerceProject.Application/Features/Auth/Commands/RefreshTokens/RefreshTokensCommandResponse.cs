using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Auth.Commands.RefreshTokens
{
    public class RefreshTokensCommandResponse
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
