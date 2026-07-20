using ECommerceProject.Application.DTOs.Auth;
using ECommerceProject.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Abstractions
{
    public interface IAuthService
    {
        // CustomResponseDto standart global dönüş tipi
        //Task<TokenDto> LoginAsync(UserLoginDto userLoginDto);

        //// Kayıt işlemince geriye değer dönmemize gerek yok (NoContent) sadece true false dönebiliriz.
        //Task RegisterAsync(UserRegisterDto userRegisterDto);

        // RefreshToken ile yeni Access Token almak için
        Task<TokenDto> CreateTokenByRefreshTokenAsync(string refreshToken);
    }
}
