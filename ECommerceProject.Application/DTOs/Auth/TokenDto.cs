using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.DTOs.Auth
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
