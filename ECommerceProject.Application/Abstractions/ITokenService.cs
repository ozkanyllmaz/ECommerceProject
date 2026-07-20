using ECommerceProject.Application.DTOs.Auth;
using ECommerceProject.Application.Features.Auth.Commands.RefreshTokens;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Abstractions
{
    // Kriptografik işlemleri yapıp bize token vermektir.
    public interface ITokenService
    {
        // Kullanıcı bilgilerini ve rollerini alıp JWT üretir.
        TokenDto CreateAccessToken(User user, IList<string> roles);

        // RefreshToken
        string GenerateRefreshToken();
    }
}
