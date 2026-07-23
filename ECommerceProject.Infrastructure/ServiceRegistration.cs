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
            })
            .AddJwtBearer(options =>
            {
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
            });
        }
    }
}
