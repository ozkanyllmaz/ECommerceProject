using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.Configurations;
using ECommerceProject.Application.Configurations;
using ECommerceProject.Application.DTOs.Auth;
using ECommerceProject.Application.Features.Auth.Commands.RefreshTokens;
using ECommerceProject.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceProject.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly CustomTokenOption _tokenOption;

        public TokenService(CustomTokenOption tokenOption)
        {
            _tokenOption = tokenOption;
        }

        public TokenDto CreateAccessToken(User user, IList<string> roles)
        {
            // Token'ın içine gömeceğimiz bilgiler.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            if(roles != null)
            {
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }
            // appsettings deki şifreyi simetrik anahtara çeviriyoruz
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));

            // şifreleme algoritması
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // token ayarlarını ve içeriği 
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer, // Token'ı üreten kişi
                audience: _tokenOption.Audience[0], // kullanıcı kitlem (mobil, web)
                claims: claims, // Kullanıcı bilgileri
                expires: DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration), // Token son kullanma tarihi
                signingCredentials: credentials // Token imzası (gizli)
                );

            // Token string formate çevrilir ve Dto olarak dönüyoruz
            var tokenHandler = new JwtSecurityTokenHandler();

            return new TokenDto
            {
                AccessToken = tokenHandler.WriteToken(jwtSecurityToken),
                AccessTokenExpiration = jwtSecurityToken.ValidTo,
                RefreshToken = GenerateRefreshToken()
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            // İşin bittiğinde kapat RAM'i yorma, belleği temizle demek için using kullandık.
            using var rng = RandomNumberGenerator.Create(); 
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);

        }
    }
}
