using ECommerceProject.Application.Abstractions;
using ECommerceProject.Infrastructure.Services;
using ECommerceProject.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ECommerceProject.Application.Security.Hashing;
using ECommerceProject.Application.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerceProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            var masterKey = configuration["Jwt:EncryptionMasterKey"];

            var encryptedSecurityKey = configuration["CustomTokenOption:SecurityKey"];
            var decryptedSecurityKey = EncryptionHelper.Decrypt(encryptedSecurityKey!, masterKey!);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["CustomTokenOption:Issuer"],
                    ValidAudiences = configuration.GetSection("CustomTokenOption:Audience").Get<string[]>(),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(decryptedSecurityKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var errorMessage = context.Exception.Message;
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var refreshTokenRepository = context.HttpContext.RequestServices.GetRequiredService<IRefreshTokenRepository>();

                        string? tokenString = context.SecurityToken switch
                        {
                            Microsoft.IdentityModel.JsonWebTokens.JsonWebToken jsonWebToken => jsonWebToken.EncodedToken,
                            _ => null
                        };

                        if(string.IsNullOrEmpty(tokenString))
                        {
                            context.Fail("Token string değeri bellekten okunamadı");
                            return;
                        }

                        var existingSession = await refreshTokenRepository.GetAccessTokenWithUserAsync(tokenString);
                        if (existingSession == null || !existingSession.IsActive(DateTime.UtcNow))
                        {
                            context.Fail("Oturum sonlandırılmış veya geçersiz token");
                        }
                    }
                };
            });
        }
    }
}
